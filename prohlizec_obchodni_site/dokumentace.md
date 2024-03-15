<style>
  #reference {
    color: gray;
    font-size: 10px;
  }
  #reference:hover {
    text-decoration: underline;
  }
</style>

# Dokumentace

## Seznam tříd

- **UI**
  - [Button.cs](#buttoncs)
  - [Control.cs](#controlcs)
  - [Color.cs](#colorcs)
- **Display**
  - [Browser.cs](#browsercs)
  - [Display.cs](#displaycs)
- **Insides**
  - [Tree.cs](#treecs)
  - [Input.cs](#inputcs)
  - [Salesman.cs](#salesmancs)
  - [SalesmenList.cs](#salesmenListcs)

  ---
<br>

# Třídy 

## UI
> Všechny třídy spojené s UI

### Button.cs <small id="reference">47 references</small>
> Řídí veškeré akce spojené s grafickou stranou tlačítek

`string Label`

`bool Selected`

`ConsoleColor ButtonColor`

`string? Shortcut`

* **Render()** <small id="reference">6 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `bool white = false`
  * > Vykreslí tlačítko do konzole
* **CreateButtons()** <small id="reference">1 reference</small>
  * Type: `internal`
  * Return type: `Button[]`
  * Static: `true`
  * Parameters: `Salesman boss, int focused`
  * > Vrátí array podřízených sformátovaný do tlačítek
* **CreateButtons()** <small id="reference">1 reference</small>
  * Type: `internal`
  * Return type: `Button[]`
  * Static: `true`
  * Parameters: `SalesmenList? list, int focused`
  * > Vrátí array obchodníku v listu sformátovaný do tlačítek

### Control.cs <small id="reference">14 references</small>
> UI Util pro zobrazování controls pod menu

`string Key`

`string Description`

* **Render()** <small id="reference">6 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `none`
  * > Vypíše specifickou keybindu

### Color.cs <small id="reference">15 references</small>
> UI Util pro jednoduché měnění barvev konzole <br>
> Obsahuje 16 metod na změnu barvy + jednu na reset

`ConsoleColor _color`

* **\[Color\]()** <small id="reference">? references</small> 
  * Type: `public`
  * Return type: `void`
  * Static: `true`
  * Parameters: `bool bg`
  * > Změní foreground color nebo background color na barvu podle názvu metody
* **Reset()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `void`
  * Static: `true`
  * Parameters: `none`
  * > Vyresetuje barvy konzole a default
---
<br>

## Display
> Všechny třídy spojené s tím co a kde se zobrazuje

### Browser.cs <small id="reference">2 references</small>
> Zde se vypisuje prakticky celý program. Třída obsahuje 2 druhy menu a k tomu UI pro nějaké třídy z [Insides](#insides)

`public static Display display`

* **MainMenu()** <small id="reference">2 references</small>
  * Type: `internal`
  * Return type: `void`
  * Static: `true`
  * Parameters: `Salesman salesman, SalesmanList? list = null`
  * > Dříve užitečná metoda, teď už jen slouží na nastavení hodnoty currentSalesman a zavolání metody [RenderMenu()](#rendermenu)
* #### **RenderMenu()** <small id="reference">7 references</small>
  * Type: `private`
  * Return type: `void`
  * Static: `true`
  * Parameters: `SalesmenList? list = null`
  * > Grafické zobrazení procházení stromu
* **RenderFileMenu()** <small id="reference">3 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `true`
  * Parameters: `SalesmenList? list = null`
  * > Grafické zobrazení procházení vlastního seznamu
* **LoadList()** <small id="reference">1 reference</small>
  * Type: `private`
  * Return type: `SalesmenList?`
  * Static: `true`
  * Parameters: `SalesmenList list`
  * > Grafické zobrazení pro metodu [LoadList()](#loadlist) z třídy [SalesmenList.cs](#salesmenlistcs)
* **CreateList()** <small id="reference">1 reference</small>
  * Type: `private`
  * Return type: `SalesmenList`
  * Static: `true`
  * Parameters: `SalesmenList list`
  * > Vytvoří nový seznam, pokud už je nějaký načtený, upozorní uživatele
* **Exit()** <small id="reference">7 references</small>
  * Type: `private`
  * Return type: `void`
  * Static: `true`
  * Parameters: `SalesmenList list, bool force = true`
  * > Zkontroluje, pokud je načtený list uložený. Pokud není, varuje uživatele a následně vypne program

### Display.cs <small id="reference">8 references</small>
> Většina prvků UI zde končí, aby se seřadila nebo dále upravila
> Obsahuje také nějaké util metody na ulehčení práce s konzolí

`Salesman CurrentSalesman`

* **Init()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `none`
  * > Pouze nastavuje hodnotu `Console.CursorVisible` na `false`
* **setPos()** <small id="reference">3 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `true`
  * Parameters: `int X, int Y, bool center = false`
  * > Nastaví pozici cursoru v konzoli na specifiké souřadnice, popřípadě na střed
* **Buttons()** <small id="reference">5 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `Button[] buttons, bool vertical = false, bool white false, bool isSub = false`
  * > Vypíše tlačítka za sebou nebo pod sebou se specifickým formátováním pro daný směr
* **WriteWithUnderline()** <small id="reference">9 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `string? text = null, Button[]? buttons = null`
  * > Vypíše text nebo array tlačítek s podtržením
* **Money()** <small id="reference">2 references</small>
  * Type: `public`
  * Return type: `string`
  * Static: `true`
  * Parameters: `int money`
  * > K číslu připojí `$` a vrátí zpět
* **DisplayControls()** <small id="reference">6 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `bool sectionSwitch = false, bool displaySwitch = true, Control custom = null, bool browserRedner = true`
  * > Vypíše předem vytvořené controls
---
<br>

## Insides
> Všechny třídy, které se starají jak to funguje ve vnitř. Tj. procházení stromu nebo získávání inputu.

### Tree.cs <small id="reference">3 references</small>
> Veškeré akce se stromem jako hledání zaměstnanců nebo nadřízených

* **GetSalesman()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `Salesman?`
  * Static: `true`
  * Parameters: `Salesman boss, int sub`
  * > Vráti result metody [FindSalesman()](#findsalesman)
* **GetSales()** <small id="reference">2 references</small>
  * Type: `public`
  * Return type: `int`
  * Static: `true`
  * Parameters: `Salesman root`
  * > Vrátí sales všech obchodníků pod `root`
* #### **FindSalesman()** <small id="reference">2 references</small>
  * Type: `private`
  * Return type: `Salesman?`
  * Static: `true`
  * Parameters: `Salesman? root, string? name, string? surname`
  * > Najde obchodníka podle jména
* **FindSupervisors()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `List<Salesman>`
  * Static: `true`
  * Parameters: `Salesman root, string name, string surname`
  * > Vrátí result metody [FindSuperisorRekurze()](#findsupervisorsrekurze)
* #### **FindSupervisorsRekurze()** <small id="reference">2 references</small>
  * Type: `private`
  * Return type: `bool`
  * Static: `true`
  * Parameters: `Salesman current, string name, string surname, List<Salesman> supervisor`
  * > Najde všechny nadřízené specifického obchodníka

### Input.cs <small id="reference">6 references</small>
> Zde se nachází veškerý input management

* **CheckForInput()** <small id="reference">0 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `true`
  * Parameters: `ConsoleKey key`
  * > Nepustí program dál dokud není zmáčklá klávesa `key`
* **CheckForInputs()** <small id="reference">2 references</small>
  * Type: `public`
  * Return type: `(int, bool)`
  * Static: `true`
  * Parameters: `ConsoleKey[] keys, ConsoleKey comboKey = ConsoleKey.Tab, ConsoleModifiers modifier = ConsoleModifiers.Shift`
  * > Vrátí index klávesy co byla zmáčknutá (podle pole `keys`) a bool hodnotu v závislosti na zmáčknutí kombinace kláves
* **CheckForInputs()** <small id="reference">4 references</small>
  * Type: `public`
  * Return type: `int`
  * Static: `true`
  * Parameters: `ConsoleKey[] keys`
  * > Vrátí index klávesy co byla zmáčknutá (podle pole `keys`)
* **CheckForKeyCombo()** <small id="reference">1 reference</small>
  * Type: `private`
  * Return type: `bool`
  * Static: `true`
  * Parameters: `ConsoleKeyInfo keyInfo, ConsoleKey inputKey, ConsoleModifiers modifier`
  * > Vrátí bool hodnotu v závislosti na zmáčknutí kombinace kláves

#### Salesman.cs <small id="reference">99+ references</small>
> Logika vytváření obchodníků se nachází zde

`string Name`

`string Surname`

`int Sales`

`List<Salesman> Subordinates`

* **AddSubordinates()** <small id="reference">99+ references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `Salesman subordinate`
  * > Přidá podřízeného

#### SalesmenList.cs <small id="reference">15 references</small>
> Zde se nachází veškerá logika vlastních seznamů

`bool Saved = false`

`DefaultName = true`

`List<Salesman> Salesmen`

`string Name`

* #### **ChangeName()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `string name`
  * > Změní název seznamu
* **AddSalesman()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `bool`
  * Static: `false`
  * Parameters: `Salesman salesman`
  * > Vrátí bool hodnotu podle toho zda-li se podařilo přidat obchodníka do seznamu. Tj. jestli už tam například není
* **RemoveSalesman()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `bool`
  * Static: `false`
  * Parameters: `Salesman salesman`
  * > Vrátí bool hodnotu podle toho zda-li se podařilo odebrat obchodníka ze seznamu. Tj. jestli tam například byl
* **SaveList()** <small id="reference">3 references</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `none`
  * > Uloží seznam do textového souboru `.tree`
* #### **LoadList()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `SalesmenList`
  * Static: `true`
  * Parameters: `string name`
  * > Načte a uloží do `SalesmenList` seznam z textového souboru podle jména
* **SaveUnderName()** <small id="reference">1 reference</small>
  * Type: `public`
  * Return type: `void`
  * Static: `false`
  * Parameters: `none`
  * > UI pro metodu [ChangeName()](#changename)