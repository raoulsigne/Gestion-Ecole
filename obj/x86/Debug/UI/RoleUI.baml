   M S B A M L   `   `   `  ����  School Brain    Ecole.listeRole   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$A  ;School Brain : Attribution des privilèges aux utilisateurs��5   	   $	��641��6L   $	��500��6Y    � WindowStartupLocation$ CenterScreen=�6e    � Icon$0 */School%20Brain;component/Images/plume.ico��6�    � 
ResizeMode$ CanMinimize=�6�   ���	#FFC8DDE2.��5      � ���	#69B0D0F5.+�5   
   �� -   5        $��gboxPrivilege��5   l   $��Liste des privilèges��6   $	��423��61   $
��Left=�6>   $��	8,121,0,0q�6Y   $	��Top=�6�   $	��463��6�   $
��Bold"�6�   .��5	      � .+�5
      �� -   5        $��lbRole��5
   8   $��Stretch=�6   $��2,6,6,6q�6F    �� SelectionMode$ Multiple=�6W    $� IsVirtualizing .  ��5      �� 5      .R�5      � $	��0,2q�6    � ColumnDefinitions 5      �� 5      $��* �6/   �� 5      $	��200 �6/   .+�5   "   �� $��chBxRole��5   *   -   5        $��Center=�5   N   ��6+   �� *newTextElement+ d� 	IsChecked 5   %   ��  �� Path$ 
IsSelected�	 �� RelativeSource	 �� *TemplatedParent+
 �� Mode$
 TwoWay=�5   U   5      5      5   
   5   	   5       �� 5#   
   -   5        $��btnEnregistrer��5#   '   $��Enregistrer��6   $��34��6=   $
��Left=�6I   $��
98,560,0,0q�6d   $	��Top=�6x   $	��128��6�   6�   �� 5$   
   -   5        $��	btnFermer��5$   "   $��Fermer��6   $��35��63   $
��Left=�6?   $��256,560,0,0q�6Z   $	��Top=�6o   $	��125��6�   6�   �� 5%   
   -   5        $��
gboxGroupe��5%   l   $��Sélectionnez le Groupe��6   $	��115��63   $
��Left=�6@   $��8,0,0,0q�6[   $	��Top=�6~   $	��463��6�   $
��Bold"�6�   .��5&      � $��84��6   .+�5'      �� -   5        $��label1��5'   `   $��Rôle ��6   $��23��6(   $
��Left=�64   $��6,2,0,0q�6O   $	��Top=�6n   $��41��6�   �� 5(      -   5        $��label2��5(   g   $��Description��6   $��24��6.   $
��Left=�6:   $��5,29,0,0q�6U   $	��Top=�6u   $	��102��6�   �� 5)      -	   5        $��txtDescription��5)   T   $��21��6   $
��Left=�6&   $��	83,31,0,0q�6A   $	��Top=�6j   $	��353��6�   {�.  6�   �� 5*      -
   5        $��cmbRole��5*   T   $��24��6   $
��Left=�6'   $��84,3,0,0q�6B   $	��Top=�6c   $	��245��6{   6�   �� 5+      -   5        $��chBxTousCocher��5+   z   $!��Cocher Tous les privilèges��6   $��20��6@   $
��Left=�6L   $��	10,59,0,0q�6g   $	��Top=�6�   $	��174��6�   6�   5,      5-      5/      50      