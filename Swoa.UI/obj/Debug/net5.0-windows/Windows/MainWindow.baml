   M S B A M L   `   `   `  ����B  >Swoa.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Swoa.UISwoa.UI  5          Swoa.UI.MainWindow   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Swoa.UI     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        $��
mainWindow��5        � Title$
  SWOA��5	      $
��1020��5
      $	��720��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $5 /pack://application:,,,/Resources/AppColors.xaml?�6%   %    5      .��5      �    AppBackground#��[  6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   .+�5    
      Swoa.UI.TimeMachine  $��2��5!       K� ZIndex$ 10��5"       � DataContext 5#      �� *(&CelestialObjectManagerVM.TimeMachineVM+   Swoa.UI.CelestialMap  5%   
   -   5          Name$ celestialMap��5&      $��1��5'      $��2��5(      $
��Auto��5*      $	��300��5+      $	��300��5,      $��20q�5-      ��5)      �� *ActualHeight+	 �� RelativeSource	 �� *Self+ 5.      �� *CelestialObjectManagerVM+!   Swoa.UI.CelestialObjectInfo  50   
   $��1��51      $��1��52       53      �� *20CelestialObjectManagerVM.SelectedCelestialObject+
 �� Mode$
 OneWay=�z�54      T� 55       T� X$	 300Q�6%   ��57      �� 58       �� 
TargetType"  6    �� Triggers 59      �� 5:       �� Binding 6&   ��  �� ElementName$ 
mainWindow�� �� Path$D >DataContext.CelestialObjectManagerVM.IsCelestialObjectSelected�$
 OneWay=� �� Value True6�    N� EnterActions 5;      �� 5<   "   .��5=   &   �� .��5>   *   W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�5?   -   	 W� To 05@   -    g� Duration$ 
0:0:00.200D�5A   -   5B   '   5C   #    N� ExitActions 5E      �� 5F   "   .��5G   &   �� .��5H   *   W� $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6:   $ 
0:0:00.200D�6�   5I   '   5J   #   5L      5N      5P      5p      5q      