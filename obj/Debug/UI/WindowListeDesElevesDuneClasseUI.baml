   M S B A M L   `   `   `  ����  School Brain/    )Ecole.UI.WindowListeDesElevesDuneClasseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$3  -School brain : Liste des Elèves de la classe��5   	   $	��673��6>   $	��887��6K    � WindowStartupLocation$ CenterScreen=�6W   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6�   .��5	      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��863��6Z   $	��635��6f   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��grdListeEleves��5
   {      AutoGenerateColumns .     
IsReadOnly . $	��526��6A   $
��Left=�6N   $��7,21,0,0q�6i   $	��Top=�6�   $	��844��6�    $� IsVirtualizing .  ��6�   �� *ListeEleves+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ No��6@      Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6L   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� *numero+6L     5      $	 Nom��6=   $	 200 6J   	 6%   �� *nom+6J     5      $ 	Matricule��6C   $ 80 6V   	 6%   �� *	matricule+6V     5      $ 
Catégorie��6C   $ 70 6V   	 6%   �� *	categorie+6V     5      $
 Sexe��6>   $ 60 6L   	 6%   �� *sexe+6L     5      $
 Etat��6>   $ 80 6L   	 6%   �� *etat+6L     5      $ date De Naissance��6M   $	 120 6h   	 6%   �� *dateNaissanceString+6h     5      $ Téléphone��6C   $ 80 6V   	 6%   �� *	telephone+6V     5      $ Téléphone du père��6C   $	 110 6^   	 6%   �� *	telParent+6^     5      $ Email��6?   $ 80 6N   	 6%   �� *email+6N     5      $ Adresse��6A   $ 80 6R   	 6%   �� *	adresse+6R   5      � 5   
   -   5        $��grid1��5   I   $��50��6   $
��Left=�6   $��	0,586,0,0q�66   $	��Top=�6V   $	��862��6n   ���	#FF86B5E8.+�5      �� -   5        $��	cmdFermer��5   `   $��Fermer��6   $��32��6&   $
��Left=�62   $��	673,8,0,0q�6M   $	��Top=�6q   $	��178��6�   6�   �� 5      -   5        $��cmdImprimer��5   `   $��Imprimer��6   $��32��6(   $
��Left=�64   $��7,8,0,0q�6O   $	��Top=�6s   $	��181��6�   6�   5      �� 5   
   -   5        $��label1��5   ^   $��Total : ��6   $��28��6#   $
��Left=�6/   $��
12,553,0,0q�6J   $	��Top=�6l   $
��Bold"�6�   $��15(�6�   �� 5   
   -   5        $��lblTotal��5   W   $��0��6   $��28��6   $
��Left=�6(   $��
75,554,0,0q�6C   $	��Top=�6g   $
��Bold"�6   $��15(�6�   5      5       