# XAML Hausübung

## Einleitung

Entwickeln Sie eine Anwendung, mit der die Fläche von geometrischen Figuren (Rechteck, Kreis, Dreieck) berechnet und addiert werden kann. Sie können das Aussehen und die Logik der Benutzerschnittstelle frei gestalten, sofern die unten angeführten Anforderungen erfüllt werden.

## Spezifikation

* Benutzer können folgende geometrische Figuren zu einer Liste (Hauptspeicher, Speichern in Datei oder DB ist *nicht* notwendig) hinzufügen:
  * Kreis (Eingabe: Radius *r*)
  * Rechteck (Eingaben: Länger der Seiten *a* und *b*)
  * Dreieck (Eingaben: Länge der Grundseite *a* und Höhe *h*)

![Figuren](shapes.svg)

* Das Programm muss den Inhalt der Liste darstellen.

* Das Programm muss die Fläche jeder hinzugefügten, geometrischen Figur berechnen und diese ebenfalls in der Liste darstellen.

* Das Programm muss die Summe der Fläche aller Figuren in der Liste berechnen und darstellen.

* Verwenden Sie C# und WPF (.NET 5).

## Bewertungskriterien

* Negativ bewertet werden Anwendungen,...
  * ...deren Code wegen Syntaxfehlern nicht kompiliert werden kann.
  * ...die nach Start sofort abstürzen ohne irgendeine sinnvolle Benutzerinteraktion zu erlauben.
  * ...deren Sourcecode nicht ordnungsgemäß über GitHub abgegeben wird (ZIP-Dateien werden *nicht* akzeptiert, Einchecken von unnötigen Ordnern wie *bin* oder *obj* führt zu Punkteabzug).

* Um positiv bewertet zu werden, muss die Anwendung mindestens folgende Funktionen korrekt ausführen können:
  * Hinzufügen der geforderten Figuren zu einer Liste im Hauptspeicher
  * Eingabe der geforderten Parameter je Figurentyp
  * Darstellen des Inhalts der Liste

* Folgende Kriterien werden bei der Benotung berücksichtigt:
  * Korrekte Flächenberechnungslogik
  * Ausgliedern der Berechnungslogik in eigene Klassen
  * Korrekte Verwendung von *Data Binding*
  * Ergonomische Gestaltung der Benutzeroberfläche (visuelles Design ist zweitrangig, Bedienbarkeit und Aussagekraft sind wichtiger)

## Testdaten mit Referenzergebnissen

| Figur    |    r |    a |    b |    h | Fläche |
| -------- | ---: | ---: | ---: | ---: | -----: |
| Kreis    |    5 |  N/A |  N/A |  N/A |  78,54 |
| Rechteck |  N/A |    3 |    4 |  N/A |  12,00 |
| Dreieck  |  N/A |    4 |  N/A |    2 |   4,00 |

Summe: 94,54
