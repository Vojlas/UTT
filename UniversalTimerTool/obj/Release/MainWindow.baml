   M S B A M L   `   `   `  ���� M  IUniversalTimerTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:UniversalTimerToolUniversalTimerTool  #    UniversalTimerTool.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:UniversalTimerTool     -     �Name$  UTT___Uniersal_timer_tool�� � Title$&  回 - UTT - Universal Timer Tool��$	��336��$��616.4��.���� -    ��Name$ tabControlMain��.@��� -    ��Name$ tabHome��$
��Home��$��Stretch=�$��Stretch=�.���  � ColumnDefinitions �� $��2* ��� $��* � � RowDefinitions �� $��5* ��� $��9* ��� $��2* �.+��� -    ��Name$ groupBoxProject��$��Project��$��0��$��0��$��2��.���  �� $��* ��� $��* ��� $��* ��� $
��0.5* ��� $
��0.5* ��� $
��0.5* � �� $
��1.3* ��� $
��1.2* �.+��� -    ��Name$ labelProjectName��$��Create new project ;)��$��0��$��3��$��0��$��18(�$
��Bold"��� -   $ labelProjectOutOfNumber��$��3��$��0��$	��0/0��$��15(��� -    ��Name$  buttonMain_PreviousProject��$��4��$
��Back��$��5q��� -   $ buttonMain_NextProject��$��5��$
��Next��$��5q��� -	   $ labelProjectTime��$��00:00:00��$��1��$��1��$��5���� -
   $ _labelProjectName��$��Total project time:��$��0��$��1���� -   $ GroupBoxUpdate��$��Update��$��0��$��1��.���  �� $
��2.5* ��� $��3* ��� $
��1.2* ��� $
��1.5* � �� $��1* ��� $��* ��� $��* ��� $��* �.+��� -   $ labelUpdateName��$��Your updates will be here��$��0��$��2��$��0��$��13(�$
��Bold"��� -   $# buttonMain_ShowUpdateSettings��$��3��$��0��$��5q�$��Settings���� -   $ labelUpdateOutOfNumber��$��2��$��0��$	��0/0��$��15(��� -   $ labelTotalUpdateTime��$��00:00:00��$��1��$��1��$��3���� -   $ _labelTotalUpdateTime��$��Total Time:��$��0��$��1���� $��0��$��1��$��1��$��2��
���   �$��0 0 0 1q��� -   $ labelWorkTime��$��00:00:00��$��1��$��2��$��3���� -   $ _labelWorkTime��$��
Work Time:��$��0��$��2���� -   $ labelTrainTime��$��00:00:00��$��1��$��3��$��3���� -   $ _labelTrainTime��$��Train Time:��$��0��$��3���� -   $ groupBoxUpdateSettings��$��Update settings��$��1��$��1��$y�Hidden=�.���  �� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $) #buttonUpdateSettings_PreviousUpdate��$��0��$��5q�$��Previous���� -   $% buttonUpdateSettings_NextUpdate��$��1��$��5q�$
��Next���� -   $$ buttonUpdateSettings_NewUpdate��$��2��$��5q�$��
New update���� -   $( "buttonUpdateSettings_CloseSettings��$��3��$��5q�$��Close settings��
��������� -   $ groupBoxNewUpdate��$��
New Update��$��1��$��1��$y�Hidden=�.���  �� $
��auto ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* ��� $
��0.5* ��� $��* �.+��� -   $  _labelNewUpdate_UpdateName��$��0��$��0��$��Update name:���� -   	 ��Name$!	 textBoxNewUpdate_UpdateName��$��1��$��0��$��2q��� -   $! _labelNewUpdate_UpdatePrice��$��0��$��1��$��Prize/Hour:���� -   $"	 textBoxNewUpdate_UpdatePrice��$��1��$��1��$��2q��� -   $&  _lavelNewUpdate_UpdateDescripton��$��0��$��2��$��Description:���� -    $'	 !textBoxNewUpdate_UpdateDescripton��$��1��$��2��$��2��
 �� MaxLines$
 32��$��2q��� -!   $$ buttonNewUpdate_CloseNewUpdate��$��0��$��5��$��Close��$
��10 2q��� -"   $# buttonNewUpdate_SaveNewUpdate��$��1��$��5��$��Save update��$
��10 2q��� -#   $ groupBoxMoneyManager��$��Money manager��$��1��$��1��$y�Visible=�.���  �� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -$   $% _labelMoneyManager_PricePerHour��$��Price per hour: ��$��0��$��2��$��0���� -%   $#	 textBoxMoneyManagerMoneyInput��$��0,00 Kč�� �� 
IsReadOnly .  $��1���� -&   $! labelMoneyManager_PriceWork��$��0,00 Kč��$��1��$��1��
��� � ��� -'   $" _labelMoneyManager_PriceWork��$��Price Work:��$��0��$��1���� -(   $" labelMoneyManager_PriceTrain��$��0,00 Kč��$��1��$��2��
���  ���� -)   $# _labelMoneyManager_PriceTrain��$��Price Train:��$��0��$��2���� $��0��$��2��$��2��
���   �$��0 0 0 1q�$	��2 0q��� -*   $, &labelMoneyManager_PriceTotalMinusTrain��$��0,00 Kč��$��1��$��3���� -+   $- '_labelMoneyManager_PriceTotalMinusTrain��$��Price Only Work��$��0��$��3���� -,   $" labelMoneyManager_PriceTotal��$��0,00 Kč��$��1��$��4���� --   $# _labelMoneyManager_PriceTotal��$��Price total Time:��$��0��$��4��� $��3��$��2�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -.   $ buttonMain_StartWork��$
��Work��$��0��$	��8 6q�
��� � ��� -/   $ buttonMain_StartTrain��$��Train��$��1��$	��8 6q�
��� ����� -0   $ buttonMain_SaveProject��$
��Save��$��3��$	��8 6q��� -1   $ buttonMain_Stop��$
��Stop��$��6��$	��8 6q�
���   �
���  ���� -2   $ tabView��$
��View��$��Stretch=�$��Stretch=�.��&   System.Windows.Controls.DataGrid  -3     Name$ dataGridViewProjects��   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Project name��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ProjektName+  $ Created�� �� *	Created+  $ Description��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *  �� *Description+4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $ buttonViewDatagridProject��-4   .��View � 	Resources ��%    �� -5   $ tabProjects��$��Project��$��Stretch=�$��Stretch=�.���  �� $��* ��� $��* �.+��� -6   $ buttonCreateNewProject��$��Create new Project��$��80 100q�$��0���� -7   $ buttonLoadNewProject��$y�Visible=�$��Load Project��$��80 100q�$��1���� -8   $ groupBoxCreateNewProject��$��1��$y�Hidden=�.���  �� $��* ��� $��* � �� $��* ��� $��* ��� $
��0.7* ��� $��2* ��� $
��0.2* ��� $
��0.7* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��Project Name: ��$��0��$��0���� -9   $	 textBoxProjectName��$��0��$��1��$��	10 2 10 2q��� $��
Created:  ��$��0��$��1��(  "System.Windows.Controls.DatePicker  -:     Name$ datePickerCreated��$��1��$��1��$��	10 2 10 2q��� -;    ��Name$ checkboxToday��$�� Today��$��1��$��2��$��Right=�$��Center=�$��0 0 25 0q��� $��Description: ��$��0��$��3���� -<   $	 textBoxDescription��$��1��$��3��$
 32���� 
���   �$��2��$��0��$��4��$��0 4 0 2q��� -=   $ checkBoxDefaultUpdate��$�� Default��$��1��$��5��$��Right=�$��Center=�$��0 0 25 0q��� $��Update Name: ��$��0��$��6���� ->   $	 textBoxUpdateName��$��1��$��6��$��	10 2 10 2q��� -?   $ buttonCreateProject��$��Create��$��1��$��10��$��	25 5 25 5q��� -@   $ buttonClose��$��Close��$��0��$��10��$��	25 5 25 5q��� -A   $��tabSettings��$��Settings��$��Stretch=�$��Stretch=�.���  �� $��* ��� $��* ��� $
��1.5* � �� $��* ��� $��* ��� $��* ��� $
��0.8* ��� $
��0.7* ��� $��* ��� $��3* �.+��� -B   $ _labelLanguage��$��	Language:���� -C    ��Name$ comboBoxLanguage��$��1��$��0��$
��20 5q�.@��� $��English���� $��	Čeština���� -D   $ _labelCurrency��$��Currency��$��0��$��1���� -E   $ comboBoxCurrency��$��1��$��1��$
��20 5q�.@��� $	��CZE���� $	��EUR���� -F   $ _labelAutosaveTime��$��Autosave time:��$��2��$��0���� -G   $ _labelProjectFolderPath��$��Project folder:��$��3��$��0���� -H   $$ _labelCurrentProjectFolderPath��$��C:\Projects\��$��3��$��1��$
��20 0q��� -I   $ buttonChangeProjectFolder��$��Change Project Folder��$��1��$��4��$
��20 2q��� -J   $ buttonSaveConfig��$
��Save��$��1��$��5��$
��20 5q�