   M S B A M L   `   `   `  ���� O  KShowProgressRecorder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShowProgressRecorderShowProgressRecorder  $    ShowProgressRecorder.EditEntry   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:ShowProgressRecorder       � Title$  	EditEntry��$	��300��$	��300�� � 	Resources  �� 
TargetType �� Property �� Value �� CornerRadius � ToolTip �� ElementName �� Path��%�  '��       '�� @     �� " ��.���  � ��$ 5,0,5,0q���  � ��$
 Left=��� " ��.���  � ��$ Center=���  � ��$ 0,2,40,2q���  �@=�ErrorTemplate �� .R�]� ��. .+��� 
��� E��$��right=�$��5,0,0,0q�$��20��$��20��$ 5�� �� $ customAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._��� $��!��$��center=�$��center=�$
��Bold"�
��������� $��customAdorner��$��Center=�.v��� 
���  ��$��1q�.��� -   	 �Name$	 grid_showDetails��
 � RowDefinitions
 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $��* ��� $��2* � J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$	 New��.+��� $��Name of the show��$��Right=�$��Bottom=�$��5q��� -   $��namebox��$��1��$	��250��$��20��$
��Left=�$��Bottom=�$��5q�����  �� UpdateSourceTrigger$ 	LostFocus=�$
 Name� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . �� $��Genre of the show��$��1��$��Right=�$��Bottom=�$��5q��� -   $��genreBox��$��1��$��1��$	��250��$��20��$
��Left=�$��Bottom=�$��5q����� $ 	LostFocus=�$ Genre� .  . �� -   $��
oldNameBox��$y�Hidden=����� *	OldName+�� $ ��Current episode you are on��$��2��$��Right=�$��Bottom=�$��5q��� -   $��currentEpBox��$��1��$��2��$	��250��$��20��$
��Left=�$��Bottom=�$��5q����� $ 	LostFocus=�$ CurrentEpisode� .  . �� $��Total number of episodes��$��3��$��Right=�$��Bottom=�$��5q��� -   $��
epCountBox��$��1��$��3��$	��250��$��20��$
��Left=�$��Bottom=�$��5q����� $ 	LostFocus=�$ EpisodesCount� .  . �� $��	Finished?��$��4��$��Right=�$��Bottom=�$��5q��� -   $��isFinishedBox��$��1��$��4��$��5q� d� 	IsChecked �� *
IsFinished+�� -	    ��Name$ button��$
��Save��$��5��$��5q�$��Right=�$	��New��