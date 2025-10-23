## Technical design Document

#### MVVM Pattern

Im Projekt habe ich ein MVVM Pattern angewendet, die Logik befindet sich im MainWindowViewModel.cs und die Ansicht im MainWindow.axaml. Dadurch ist 

#### Repository Pattern

Im Projekt habe ich auch das Repository Pattern benutzt. Das ist zwar mit mehr Aufwand verbunden, allerdings ist so die GUI komponente besser und sauber von der
Data Komponente getrennt.

#### Binding

Für das Binding zwischen dem ViewModel und der View habe ich benutzt:

1. Observable Property: für Properties
2. Relay Commands: für Methoden


#### Key Classes

- VocabRepository: Ist eine statische Klasse, die vom ViewModel benutzt werden kann, um auf die Datenbank neue Elemente hinzuzufügen und Elemente zu lesen.

#### DatenModell

- VocabSet: Besteht aus einem Name und einer Collection die zugewiesene Vokalbelkarten speichert

- VocabCard: Wird einem Vocab Set über VocabSetId zugewiesen, außerdem besitzt es die Properties: Language, Orgininal, Translation, welche required sind und einene ExampleSentence und ein DifficultyLevel welche freiwillig sind und nicht angegeben werden müssen.

#### Design:

Ich habe mich für einen Grid als Layout Methode entschieden, da ich damit am besten die Positionen für meine Elemente festlegen kann.

Für die Anzeige meiner Vokabelkarte habe ich wieder einen Grid genommen, um die Elemente auf der Karte gut ausrichten zu können. Und in diesem Grid habe ich auch Stack Panels benutzt, damit die Elemente vertikal ausgerichtet sind.

Auch für das Feld zum Erstellen von Vokabelsets und für Vokabelkarten, habe ich aus dem selben Grund ein StackPanel benutzt.






