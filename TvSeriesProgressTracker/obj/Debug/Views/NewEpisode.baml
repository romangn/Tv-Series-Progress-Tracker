   M S B A M L   `   `   `  ����R  NTvSeriesProgressTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TvSeriesProgressTrackerTvSeriesProgressTracker  5   	   (    "TvSeriesProgressTracker.NewEpisode   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:TvSeriesProgressTracker     5   	     � Title$  Add new episode��5   	   $	��640��6!   $	��480��6.    � 	Resources 5	       �� 
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
   �� $��Title of the episode��6   $��Right=�56      $��Bottom=�57      $��5q�58      �� 59   
   -   5        $��titlebox��5:      $��1��59      $	��250��5=      $��20��5>      $
��Left=�5?      $��Bottom=�5@      $��5q�5A      ��5<      ��  �� UpdateSourceTrigger$ 	LostFocus=�$ Title� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . 5A      �� 5B   
   $��Season��6   $��1��5C      $��Right=�5D      $��Bottom=�5E      $��5q�5F      �� 5G   
   -   5        $��	seasonBox��5I      $��1��5G      $��1��5H      $	��250��5L      $��20��5M      $
��Left=�5N      $��Bottom=�5O      $��5q�5P      ��5K      �� $ 	LostFocus=�$ Season� .  . 5P      �� 5Q   
   $��Episode number��6   $��2��5R      $��Right=�5S      $��Bottom=�5T      $��5q�5U      �� 5V   
   -   5        $��
episodeBox��5X      $��1��5V      $��2��5W      $	��250��5[      $��20��5\      $
��Left=�5]      $��Bottom=�5^      $��5q�5_      ��5Z      �� $ 	LostFocus=�$ Episode� .  . 5_      �� 5`   
   -   5        $��idElem��5a      $y�Hidden=�5b      ��5`      �� *ShowId+5b      �� 5c   
   -   5         ��Name$ button��5d      $
��Save��5c      $��3��5e      $��5q�5f      $��Right=�5g      $	��New��5i      5j      5k      