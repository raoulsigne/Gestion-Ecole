   M S B A M L   `   `   `  ����  School Brain    	Ecole.App   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        �� 	Resources  5          MyGlassGradientStopsResource � Color � Offset  GrayBlueGardientBrush �� 
StartPoint �� EndPoint  CustomButton �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� RadiusX �� RadiusY J� Opacity J� RenderTransformOrigin J� BitmapEffect ��Name �� Triggers P� Property P� Value �� 
TargetName �� Radius :� RoutedEvent ��Name �� 
TargetName �� TargetProperty	 W� By g� Duration �� BeginStoryboardName��%  &	        &	 K    &	     7  7  � 5   
   � 5      $ 
WhiteSmoke��6   $	 0.2Q�6.   � 5      $ Transparent��6   $	 0.4Q�6/   � 5	      $ 
WhiteSmoke��6   $	 0.5Q�6.   � 5
      $ Transparent��6   $
 0.75Q�6/   � 5      $ 
WhiteSmoke��6   $	 0.9Q�6.   � 5      $ Transparent��6   $ 1Q�6/   5      �� 5   
   $	 0,0&�5      $	 1,1&�6.   .��5      � $ DarkCyan��6   $ 0Q�6,   � 5      $ #CCCCFF��6   $	 0.5Q�6+   � 5      $ DarkCyan��6   $ 1Q�6,   5      �� 5   
   " ��6%   .�5      ��  � ��8   6+   �� 5       � ��$ 80��6&   �� 5       � ��$ 10q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      � #��z��6   #��z��6?   }�. .+�5      �� $	 outerRectangle��6(   $��Stretch=�6@   $��Stretch=�5    '   #��z��6C   $
 20��5!   '   $ 20��64   $��5��6A   
������ 6U   �� 5#      $	 innerRectangle��6(   $��Stretch=�6@   $��Stretch=�5$   '   
������ $��20��6X   #��z��5%   '   $
 20��6K   $ 20��6X   �� 5'      $	 	glassCube��6(   $��Stretch=�6;   $��Stretch=�5(   '   $��2��5)   '   $
 10��6;   $ 10��6H   $ 0Q�6U   8�� $ 0.5,0.5&�5+   '   ��5,   "   �� 5-   &   $ 0.5,0&�6:   $ 0.5,1&�6M   ��5/   .   � $	 0.0Q�6;   $ 	LightBlue��6H   � 50   .   $	 1.0Q�6;   $
 Gray��6H   52   '   z�55   "   V� 56   &   .~�57   *   �� �� 58   *   59   '    5<   "   �� 5=   &   5?      ]� 5@      $!��myContentPresenterDockPanel��6(   .+�5A   "   �� $ myContentPresenter��63   $��20q�6O   #��z��5B   2   
���   �6W   5C      5D       5E      P� 5F       �@J�IsMouseOver . . �5G   "   �� $ outerRectangle��6F   # � ��5H       � ���� 5I   "   $ 	glassCube��6P    �@J�Opacity$ 1Q�6F   6P   �� 5J   "   $ myContentPresenter��6R    �@J�BitmapEffect 5K   &   �� 5L   *   $ 1Q�6;   5N   #   5O      P� 5P       �@J�	IsFocused . . �5Q   "   �� $ 	glassCube��6P    �@J�Opacity$ 1Q�6F   6P   �� 5R   "   $ outerRectangle��6E   # � ��5S   )    � ���� 5T   "   $ 	glassCube��6P    �@J�Opacity$ 1Q�6F   6P   5U      :� 5V      $ Mouse.MouseEnter��6+   [�5W   "   �� 5X   &   $ mouseEnterBeginStoryboard��66   .��5Y   *   �� .��5[   .   W� $ 	glassCube��6>   $� �(Rectangle.RenderTransform).(TransformGroup.Children)[0].                                                                          (ScaleTransform.ScaleX)�5\   >   $
 -0.1X�5^   >   $ 0:0:0.5D�6H   W� 5`   .   $ 	glassCube��6>   $� �(Rectangle.RenderTransform).(TransformGroup.Children)[0].                                                                          (ScaleTransform.ScaleY)�6`   $
 -0.1X�5b   >   $ 0:0:0.5D�6H   5c   +   5d   '   5f      :� 5g      $ Mouse.MouseLeave��6+   [�5h   "   �� 5i   &   $ mouseEnterBeginStoryboard��65   5k      :� 5l      $ Button.Click��6+   [�5m   "   �� 5n   &   .��5o   *   �� .��5p   .   W� $ 	glassCube��6>   $V P(Rectangle.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)�5q      $	 360X�5s      $ 0:0:0.5D�6   5t   +   5u   '   5w      5y      5|      5}      5~      5      