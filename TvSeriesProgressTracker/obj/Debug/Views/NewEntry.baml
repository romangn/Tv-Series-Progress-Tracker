   M S B A M L   `   `   `  ����R  NTvSeriesProgressTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShowProgressRecorderShowProgressRecorder  5   	   &     TvSeriesProgressTracker.NewEntry   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShowProgressRecorder     5   	     � Title$  	New entry��5   	   $	��640��6   $	��480��6(    � 
ResizeMode$ NoResize=�5	   	    � 	Resources 5
       �� 
TargetType �� Property �� Value �� CornerRadius � ToolTip �� ElementName	 �� Path��%  '��       '�� s     �� 5   
   " ��6   .�5      ��  � ��$ 5,0,5,0q�6'   �� 5       � ��$
 Left=�64   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 0,2,40,2q�6'   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      ]� ��. .+�5      �� 
��� E��$��right=�6<   $��5,0,0,0q�6S   $��20��5   !   $��20��6,   $ 5��68    5   !   �� $ customAdorner��$8	 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5   "   �� $��!��6,   $��center=�65   $��center=�6P   $
��Bold"�5   $   
�������66   5      �� 5      $��customAdorner��68   $��Center=�6M   .v�5   "   �� 
���  ��$��1q�6;   5      5       5!      5#      5$      5%      .��5&      � -   5        
 �Name$
 grid_showData��5&       � RowDefinitions 5'   
   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   �� 5-      $
��Auto �6   �� 5.      $
��Auto �6    � ColumnDefinitions 50   
   �� 51      $��* �6   �� 52      $��2* �6    J� CommandBindings 54   
   )  #System.Windows.Input.CommandBinding  55      -   5           Command$	 New��55      57      .+�59   
   �� $��Name of the show��6   $��Right=�5:      $��Bottom=�5;      $��5q�5<      �� 5=   
   -   5        $��namebox��5>      $��1��5=      $	��250��5A      $��20��5B      $
��Left=�5C      $��Bottom=�5D      $��5q�5E      ��5@      ��  �� UpdateSourceTrigger$ 	LostFocus=�$	 Title� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . 5E      �� 5F   
   $��Genre of the show��6   $��1��5G      $��Right=�5H      $��Bottom=�5I      $��5q�5J      �� 5K   
   -   5        $��genreBox��5M      $��1��5K      $��1��5L      $	��250��5P      $��20��5Q      $
��Left=�5R      $��Bottom=�5S      $��5q�5T      ��5O      �� $ 	LostFocus=�$	 Genre� .  . 5T      �� 5U   
   $��Overall seasons��6   $��2��5V      $��Right=�5W      $��Bottom=�5X      $��5q�5Y      �� 5Z   
   -   5        $��overallSeasonsBox��5\      $��1��5Z      $��2��5[      $	��250��5_      $��20��5`      $
��Left=�5a      $��Bottom=�5b      $��5q�5c      ��5^      �� $ 	LostFocus=�$	 totalSeasons� .  . 5c      �� 5d   
   $��Current season��6   $��3��5e      $��Right=�5f      $��Bottom=�5g      $��5q�5h      �� 5i   
   -   5        $��currentSeasonBox��5k      $��1��5i      $��3��5j      $	��250��5n      $��20��5o      $
��Left=�5p      $��Bottom=�5q      $��5q�5r      ��5m      �� $ 	LostFocus=�$	 CurrentSeason� .  . 5r      �� 5s   
   $��Current episode��6   $��4��5t      $��Right=�5u      $��Bottom=�5v      $��5q�5w      �� 5x   
   -   5        $��currentEpBox��5z      $��1��5x      $��4��5y      $	��250��5}      $��20��5~      $
��Left=�5      $��Bottom=�5�      $��5q�5�      ��5|      �� $ 	LostFocus=�$	 CurrentEpisode� .  . 5�      �� 5�   
   -   5         ��Name$ button��5�      $
��Save��5�      $��5��5�      $��5q�5�      $��Right=�5�      $	��New��5�      5�      5�      