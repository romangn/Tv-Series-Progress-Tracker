   M S B A M L   `   `   `  ����R  NTvSeriesProgressTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShowProgressRecorderShowProgressRecorder  5   	   '    !TvSeriesProgressTracker.EditEntry   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShowProgressRecorder     5   	     � Title$  	EditEntry��5   	   $
��1024��6   $	��768��6)    � 	Resources 5	       �� 
TargetType �� Property �� Value �� CornerRadius � ToolTip �� ElementName �� Path��%  '��       '�� s     �� 5
   
   " ��6   .�5      ��  � ��$ 5,0,5,0q�6'   �� 5       � ��$
 Left=�64   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 0,2,40,2q�6'   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      ]� ��. .+�5      �� 
��� E��$��right=�6<   $��5,0,0,0q�6S   $��20��5   !   $��20��6,   $ 5��68    5   !   �� $ customAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5   "   �� $��!��6,   $��center=�65   $��center=�6P   $
��Bold"�5   $   
�������66   5      �� 5      $��customAdorner��68   $��Center=�6M   .v�5   "   �� 
���  ��$��1q�6;   5      5      5       5"      5#      5$      .��5%      � -   5        	 �Name$	 grid_showDetails��5%      
 � RowDefinitions
 5&   
   �� 5'      $
��Auto �6   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   �� 5-      $
��Auto �6    � ColumnDefinitions 5/   
   �� 50      $��* �6   �� 51      $��2* �6    J� CommandBindings 53   
   )  #System.Windows.Input.CommandBinding  54      -   5           Command$	 New��54      56      .+�58   
   �� $��Name of the show��6   $��Right=�59      $��Bottom=�5:      $��5q�5;      �� 5<   
   -   5        $��namebox��5=      $��1��5<      $	��250��5@      $��20��5A      $
��Left=�5B      $��Bottom=�5C      $��5q�5D      ��5?      ��  �� UpdateSourceTrigger$ 	LostFocus=�$ Title� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . 5D      �� 5E   
   $��Genre of the show��6   $��1��5F      $��Right=�5G      $��Bottom=�5H      $��5q�5I      �� 5J   
   -   5        $��genreBox��5L      $��1��5J      $��1��5K      $	��250��5O      $��20��5P      $
��Left=�5Q      $��Bottom=�5R      $��5q�5S      ��5N      �� $ 	LostFocus=�$ Genre� .  . 5S      �� 5T   
   -   5        $��
oldNameBox��5T      $y�Hidden=�5V      ��5U      �� *	OldName+5V      �� 5W   
   $��Total seasons��6   $��2��5X      $��Right=�5Y      $��Bottom=�5Z      $��5q�5[      �� 5\   
   -   5        $��seasonsCountBox��5^      $��1��5\      $��2��5]      $	��250��5a      $��20��5b      $
��Left=�5c      $��Bottom=�5d      $��5q�5e      ��5`      �� $ 	LostFocus=�$ totalSeasons� .  . 5e      �� 5f   
   $��Current season��6   $��3��5g      $��Right=�5h      $��Bottom=�5i      $��5q�5j      �� 5k   
   -   5        $��currentSeasonBox��5m      $��1��5k      $��3��5l      $	��250��5p      $��20��5q      $
��Left=�5r      $��Bottom=�5s      $��5q�5t      ��5o      �� $ 	LostFocus=�$ CurrentSeason� .  . 5t      �� 5u   
   $ ��Current episode you are on��6   $��4��5v      $��Right=�5w      $��Bottom=�5x      $��5q�5y      �� 5z   
   -   5        $��currentEpBox��5|      $��1��5z      $��4��5{      $	��250��5      $��20��5�      $
��Left=�5�      $��Bottom=�5�      $��5q�5�      ��5~      �� $ 	LostFocus=�$ CurrentEpisode� .  . 5�      �� 5�   
   $��	Finished?��6   $��5��5�      $��Right=�5�      $��Bottom=�5�      $��5q�5�      �� 5�   
   -	   5        $��isFinishedBox��5�      $��1��5�      $��5��5�      $��5q�5�       d� 	IsChecked 5�      �� *
IsFinished+5�      �� 5�   
   -
   5         ��Name$ button��5�      $
��Save��5�      $��6��5�      $��5q�5�      $��Right=�5�      $	��New��5�      5�      5�      5�      