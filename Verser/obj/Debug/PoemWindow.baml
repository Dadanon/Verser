   M S B A M L   `   `   `  ????A  =Verser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:VerserVerser  5   	       Verser.PoemWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Verser     5   	   -   5          ?Name$  poemW??5   1    ? Title$ Verser??6	   $	??800??6   $	??300??6%   $	??800??6@   $	??300??6P   ??5	      ?? 5
   
    ?? ImageSource$ /back/poemW.jpg??6    j? Stretch$
 None=?63    ? 	Resources 5         Verser.MinusFiftyConverter   minusConverter??%   &	          5   
   6$   5      .??5      ? -   5         ?Name$ poemGrid??5       ? RowDefinitions 5   
   ?? 5      ?? 5      $??9* ?6   ?? 5      .+?5   
   ?? -   5         ??Name$ TitleRow??5      $??0??6$   $??10q?61   $??Center=?5      $??Center=?64   $??14(?5      $??Century Gothic)?6    ??5      ??  ?? ElementName$ poemW??	 ?? Path$	 Width?
 ?? UpdateSourceTrigger$
 PropertyChanged=? ?? 	Converter# [  .??6?    5       ?? 
TargetType ?? Property ?? Value??%_   '??       ?? 5      " ??6   .?5      ??  ?@??CornerRadius$ 15??65   5      5      .??6!   5      ?? 5   
   -   5         ??Name$ 
HomeButton??5      $??0q?6%   $??3q?61   
???????$??2??5    $   $??Center=?61   $??Center=?5!      .??5"      ?? $!??/icons/button_home_w100.png??6   $	??100??69   $??50??6E    5#      ??%^   '??       ?? 5$      " ??6   .?5%      ??  ?@??CornerRadius$ 3??65   5&      5'      5(      ?? 5)   
   -   5         ??Name$ 
TextBorder??5)      $??1??6%   $??20q?62    ?? CornerRadius$ 15??6>   ??. 
?????????5*      ?? $ poemW??$	 Width?$
 PropertyChanged=?# [  5,      5-      5.      