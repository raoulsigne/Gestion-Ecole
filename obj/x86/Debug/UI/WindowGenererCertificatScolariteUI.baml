   M S B A M L   `   `   `  ����  School Brain1    +Ecole.UI.WindowGenererCertificatScolariteUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$?  9School brain : Génération des certificats de scolarité��5   	   $	��341��6H   $	��633��6U    � WindowStartupLocation$ CenterScreen=�6a   ���	#FFD0DAE5.��5	      �� ���	#FFC8DDE2 �� Orientation$ Vertical=�6(   $	��293��6?   .+�5
   
   �� $ 
Horizontal=�6   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��193��6:   $	��396��6X   $
��15,5q�6d   .��5      � -   5        $��grid1��5   $   $	��160��6   $	��376��61   ���	#FF86B5E8 � RowDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��112* �6+   �� 5      $
��218* �6+   .+�5      �� -   5        $��txtAnneeScolaire��5   V   $��25��6   $
��Left=�6*   $��0,8,0,0q�6E   $	��Top=�6n   $��57��6�   $��1��6�   6  �� 5      -   5        $��label1��5   q   $��/��6   $��1��6(   $��26��68   $
��Left=�6D   $��57,8,0,0q�6_   $	��Top=�6   $��20��6�   $��2��6�   �� 5      -   5        $��txtAnnee��5   i   {�.  $��25��60   $
��Left=�6<   $��72,7,0,0q�6W   $	��Top=�6y   $��54��6�   $��1��6�   6�   �� 5      -   5        $��txtMatricule��5   E   $��25��6   $
��Left=�6*   $	��Top=�6Y   $	��126��6q   $��1��6}   $��3��6�   6�   �� 5      -   5        $��lblAnnee��5   S   $��Année��6   $��28��6,   $
��Left=�68   $	��Top=�6c   $��97��6{   $��0,6,0,0q�6�   �� 5      -   5        $��lblMatricule��5   W   $��	Matricule��6   $��28��60   $
��Left=�6<   $	��Top=�6k   $��95��6�   $��2��6�   $��0,28,0,0q�6�   $��2��6�   �� 5      -	   5        $��
cmdAnnuler��5   i   $��Annuler��6   $��25��6/   $
��Left=�6;   $��	131,1,0,0q�6V   $	��Top=�6{   $��89��6�   $��4��6�   $��1��6�   �� 5      -
   5        $��
cmdValider��5   g   $��Valider��6   $��25��6/   $
��Left=�6;   $��6,1,0,0q�6V   $	��Top=�6y   $	��106��6�   $��4��6�   �� 5       -   5        $��	lblClasse��5    T   $��Classe��6   $��28��6-   $
��Left=�69   $	��Top=�6e   $��1��6}   �� 5!      -   5        $��	cmbClasse��5!   V   $��1��6   $��26��6/   $
��Left=�6;   $	��Top=�6g   $	��126��6   $��1��6�   $��0,6,0,0q�6�   6�   �� 5"      -   5        $��label2��5"   ^   $	��Nom��6   $��2��6*   $��28��67   $
��Left=�6C   $	��Top=�6l   �� 5#      -   5        $��cmbEleve��5#   W   $��26��6   $
��Left=�6+   $��0,2,0,0q�6F   $	��Top=�6g   $	��242��6   $��1��6�   $��2��6�   6�   5$      5%      �� 5&      -   5        $��
imageEleve��5&   !   $	��140��6   $
��Fill=�63   $	��164��6B   $��15,15q�6N   5'      �� 5(   
   -   5        $��lblInfoEleve��5(   '   $�� ��6   $��28��6   $	��472��6;   $��
-100,0,0,0q�6G   $��Center=�6[   $
��Bold"�6}   $��Normal$�6�   $��Normal&�6�    �� Target 6�   �� 6�   � 5)   
   -   5        $
��grid��5)      $��40��6   ���	#FF86B5E8$
��0,20q�6>    5*      �� 5+      $	��20* �6     5-      �� 5.      .+�50      �� -   5        $��	cmdFermer��50   `   $��Fermer��6   $��28��6&   $
��Left=�62   $��	224,6,0,0q�6M   $	��Top=�6q   $	��152��6�   6�   51      52      53      