   M S B A M L   `   `   `  ����R  NTvSeriesProgressTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TvSeriesProgressTrackerTvSeriesProgressTracker  5   	   )    #TvSeriesProgressTracker.EditEpisode   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:TvSeriesProgressTracker     5   	     � Title$  Edit Episode��5   	   $	��640��6   $	��480��6+    � 	Resources 5	       �� 
TargetType �� Property �� Value �� CornerRadius � ToolTip �� ElementName �� Path��%  '��       '�� s     �� 5
   
   " ��6   .�5      ��  � ��$ 5,0,5,0q�6'   �� 5       � ��$
 Left=�64   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 0,2,40,2q�6'   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      ]� ��. .+�5      �� 
��� E��$��right=�6<   $��5,0,0,0q�6S   $��20��5   !   $��20��6,   $ 5��68    5   !   �� $ customAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5   "   �� $��!��6,   $��center=�65   $��center=�6P   $
��Bold"�5   $   
�������66   5      �� 5      $��customAdorner��68   $��Center=�6M   .v�5   "   �� 
���  ��$��1q�6;   5      5      5       5"      5#      5$      .��5%      � -   5        	 �Name$	 grid_episodeData��5%      
 � RowDefinitions
 5&   
   �� 5'      $
��Auto �6   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6    � ColumnDefinitions 5,   
   �� 5-      $��* �6   �� 5.      $��2* �6    J� CommandBindings 50   
   )  #System.Windows.Input.CommandBinding  51      -   5           Command$	 New��51      53      .+�55   
   �� -   5        $��
oldNameBox��55      $y�Hidden=�57      ��56      �� *	OldName+57      �� 58   
   $��Title of the episode��6   $��Right=�59      $��Bottom=�5:      $��5q�5;      �� 5<   
   -   5        $��titlebox��5=      $��1��5<      $	��250��5@      $��20��5A      $
��Left=�5B      $��Bottom=�5C      $��5q�5D      ��5?      ��  �� UpdateSourceTrigger$ 	LostFocus=�$ Title� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . 5D      �� 5E   
   $��Season��6   $��1��5F      $��Right=�5G      $��Bottom=�5H      $��5q�5I      �� 5J   
   -   5        $��	seasonBox��5L      $��1��5J      $��1��5K      $	��250��5O      $��20��5P      $
��Left=�5Q      $��Bottom=�5R      $��5q�5S      ��5N      �� $ 	LostFocus=�$ Season� .  . 5S      �� 5T   
   $��Episode number��6   $��2��5U      $��Right=�5V      $��Bottom=�5W      $��5q�5X      �� 5Y   
   -   5        $��
episodeBox��5[      $��1��5Y      $��2��5Z      $	��250��5^      $��20��5_      $
��Left=�5`      $��Bottom=�5a      $��5q�5b      ��5]      �� $ 	LostFocus=�$ Episode� .  . 5b      �� 5c   
   -   5        $��idElem��5d      $y�Hidden=�5e      ��5c      �� *ShowId+5e      �� 5f   
   -   5         ��Name$ button��5g      $
��Save��5f      $��3��5h      $��5q�5i      $��Right=�5j      $	��New��5l      5m      5n      