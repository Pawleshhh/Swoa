using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class EventAssert
    {

        public static void EventRises<T>(Action<EventHandler<T>> attachEvent, Action riseEvent)
            where T : EventArgs
        {
            CheckIfEventRises(attachEvent, riseEvent, true);
        }

        public static void EventDoesNotRise<T>(Action<EventHandler<T>> attachEvent, Action riseEvent)
            where T : EventArgs
        {
            CheckIfEventRises(attachEvent, riseEvent, false);
        }

        public static void EventArgsEqual<T>(Action<EventHandler<T>> attachEvent, Action riseEvent, Predicate<T> match)
            where T : EventArgs
        {
            CheckIfEventArgsAreEqual(attachEvent, riseEvent, match, true);
        }

        public static void EventArgsAreNotEqual<T>(Action<EventHandler<T>> attachEvent, Action riseEvent, Predicate<T> match)
            where T : EventArgs
        {
            CheckIfEventArgsAreEqual(attachEvent, riseEvent, match, false);
        }

        private static void CheckIfEventArgsAreEqual<T>(Action<EventHandler<T>> attachEvent, Action riseEvent, Predicate<T> match, bool expected)
            where T : EventArgs
        {
            EventRiser<T> eventRiser = new EventRiser<T>();
            attachEvent(eventRiser.AttachedEvent);
            riseEvent();

            bool result = match(eventRiser.Result);

            if (expected != result)
            {
                if (result)
                    throw new AssertFailedException("EventArgs are equal");
                else
                    throw new AssertFailedException("EventArgs are not equal");
            }
        }

        private static void CheckIfEventRises<T>(Action<EventHandler<T>> attachEvent, Action riseEvent, bool expected)
            where T : EventArgs
        {
            EventRiser<T> eventRiser = new EventRiser<T>();

            attachEvent(eventRiser.AttachedEvent);
            riseEvent();

            if (!eventRiser.EventRised && expected)
                throw new AssertFailedException("Event did not rise");
            else if (eventRiser.EventRised && !expected)
                throw new AssertFailedException("Event did rise");
        }

        class EventRiser<T> where T : EventArgs
        {

            public T Result { get; private set; }
            public bool EventRised { get; private set; }

            public void AttachedEvent(object sender, T e)
            {
                EventRised = true;
                Result = e;
            }

        }

    }
}
