   M S B A M L   `   `   `  ����  School Brain$    Ecole.UI.RapportNotificationUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � ���	#FFC8DDE2$	��520��6"   .+�5	   
   &   System.Windows.Controls.DataGrid  -   5        $��	grdEchecs��5	   j       AutoGenerateColumns  .  $	��250��6/   $
��Left=�6<   $��	12,95,0,0q�6W   $	��Top=�6{   $	��496��6�   "   VerticalScrollBarVisibility$ Visible=�5
   +   ��6   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Numéro��6@      
IsReadOnly . -   5        grdCode��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 180 5   >   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *numero+5        5      $ Nom / Matricule��6=    . -   5        
grdNom��$	 290 5   =    5   %   �� *nom+5      5      �� 5   
   -   5        $��label1��5   |   ���	#FF86B5E8$��LISTE DES ECHECS��6'   $��28��6B   $
��Left=�6N   $��	12,69,0,0q�6i   $	��Top=�6�   $	��496��6�   �� 5   
   -   5        $��label2��5   k   $��Nombre de sms réussis ��6   $��28��61   $
��Left=�6=   $��	12,21,0,0q�6X   $	��Top=�6y   �� 5   
   -   5        $��	txtNombre��5   _   {�.  $��23��6$   $
��Left=�60   $��
154,25,0,0q�6K   $	��Top=�6p   $	��199��6�   $��Times New Roman)�6�   $
��Bold"�6�   $��Center=�6�   $
��Left=�6�   �� 5   
   -   5        $��	btnExport��5   i   $��Exporter sur Excel��6   $��23��6.   $
��Left=�6:   $��
378,24,0,0q�6U   $	��Top=�6z   $	��130��6�   6�   5      5      