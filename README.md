# APBD-Cw1-s29820

## Opis projektu

Projekt przedstawia konsolową aplikację napisaną w języku C#, służącą do obsługi uczelnianej wypożyczalni sprzętu.

Aplikacja umożliwia:
- dodawanie różnych typów sprzętu,
- dodawanie różnych typów użytkowników,
- wypożyczanie i zwracanie sprzętu,
- kontrolę dostępności sprzętu,
- pilnowanie limitów aktywnych wypożyczeń,
- naliczanie kar za opóźniony zwrot,
- generowanie raportu podsumowującego.

Projekt został przygotowany w stylu obiektowym, z podziałem na model domenowy, logikę biznesową, warstwę danych w pamięci oraz prostą warstwę uruchomieniową w konsoli.

---

## Wymagania

Do uruchomienia projektu potrzebne są:
- .NET 8 SDK
- system Windows / Linux / macOS
- terminal lub IDE obsługujące projekty C#

---

## Jak uruchomić

1. Otworzyć katalog projektu.
2. W terminalu wykonać:

```bash
dotnet build
dotnet run
```

Po uruchomieniu aplikacja wykona scenariusz demonstracyjny i wypisze wynik w konsoli.
Scenariusz pokazuje m.in. listę dostępnego sprzętu, poprawne wypożyczenie, próbę wypożyczenia niedostępnego sprzętu, przekroczenie limitu wypożyczeń, aktywne wypożyczenia użytkownika, oznaczenie sprzętu jako niedostępnego, przeterminowane wypożyczenia oraz raport końcowy.

---

## Struktura projektu

### `Domain`
Zawiera model domenowy aplikacji:
- bazowe klasy abstrakcyjne `Equipment` i `User`,
- klasy dziedziczące:
  - `Laptop`
  - `Projector`
  - `Camera`
  - `Student`
  - `Employee`
- klasę `Rental`,
- enumy `EquipmentStatus` i `UserType`.

### `Services`
Zawiera logikę biznesową:
- `EquipmentService`
- `UserService`
- `RentalService`
- `ReportService`

oraz polityki:
- `UserLimitPolicy`
- `SimplePenaltyPolicy`

### `Data`
Zawiera `InMemoryStore`, czyli prosty magazyn danych w pamięci aplikacji.

### `Exceptions`
Zawiera własne wyjątki biznesowe:
- `BusinessRuleException`
- `EquipmentNotAvailableException`
- `UserLimitExceededException`
- `RentalNotFoundException`

### `ConsoleUI`
Zawiera `DemoScenario`, czyli scenariusz demonstracyjny uruchamiany po starcie aplikacji.

### `Program.cs`
Pełni rolę punktu wejścia aplikacji. Tworzy potrzebne obiekty i uruchamia scenariusz demonstracyjny. Nie zawiera logiki biznesowej.

---

## Najważniejsze decyzje projektowe

### 1. Klasy abstrakcyjne dla wspólnego modelu
Klasy `Equipment` i `User` zostały zaprojektowane jako abstrakcyjne, ponieważ reprezentują wspólne cechy różnych typów sprzętu i użytkowników.

Dzięki temu:
- uniknięto duplikacji kodu,
- zachowano czytelne dziedziczenie,
- łatwiej rozbudować projekt o kolejne typy.

### 2. Wydzielenie logiki biznesowej do serwisów
Operacje biznesowe, takie jak wypożyczenie, zwrot, walidacja limitów czy generowanie raportu, zostały umieszczone w osobnych serwisach zamiast w `Program.cs`.

Dzięki temu:
- kod jest bardziej czytelny,
- odpowiedzialności klas są rozdzielone,
- łatwiej testować i rozwijać projekt.

### 3. Wydzielenie polityk biznesowych
Zasady dotyczące:
- maksymalnej liczby aktywnych wypożyczeń,
- naliczania kary za opóźnienie

zostały przeniesione do osobnych klas:
- `UserLimitPolicy`
- `SimplePenaltyPolicy`

Pozwala to łatwo zmienić reguły bez przebudowy głównych serwisów.

### 4. Przechowywanie danych w pamięci
Dane są przechowywane w `InMemoryStore`, ponieważ projekt ma charakter demonstracyjny i nie wymaga bazy danych.

To upraszcza aplikację i pozwala skupić się na modelu obiektowym oraz logice biznesowej.

---

## Gdzie w projekcie widać zasady OOP, kohezję i niski coupling

### OOP
W projekcie wykorzystano podstawowe zasady programowania obiektowego:
- **dziedziczenie**:
  - `Laptop`, `Projector`, `Camera` dziedziczą po `Equipment`
  - `Student`, `Employee` dziedziczą po `User`
- **enkapsulację**:
  - właściwości mają kontrolowany dostęp,
  - zmiana stanu obiektów odbywa się przez metody
- **abstrakcję**:
  - wspólne cechy zostały wyniesione do klas bazowych
- **polimorfizm**:
  - kolekcje przechowują obiekty bazowych typów (`Equipment`, `User`)

### Kohezja
Każda klasa ma jedną główną odpowiedzialność:
- `EquipmentService` obsługuje sprzęt,
- `UserService` obsługuje użytkowników,
- `RentalService` obsługuje wypożyczenia i zwroty,
- `ReportService` generuje raport,
- polityki odpowiadają wyłącznie za reguły biznesowe.

### Niski coupling
Poszczególne elementy systemu są ze sobą powiązane w ograniczonym stopniu:
- `Program.cs` tylko składa aplikację,
- serwisy korzystają z `InMemoryStore` i interfejsów polityk,
- logika limitów i kar nie jest rozrzucona po wielu klasach.

Dzięki temu projekt jest bardziej czytelny i łatwiejszy do rozwijania.

---

## Co pokazuje scenariusz demonstracyjny

Po uruchomieniu aplikacji wykonywany jest scenariusz demonstracyjny, który pokazuje:

1. dodanie użytkowników różnych typów,
2. dodanie kilku egzemplarzy różnych typów sprzętu,
3. poprawne wypożyczenie sprzętu,
4. próbę wypożyczenia sprzętu niedostępnego,
5. próbę przekroczenia limitu aktywnych wypożyczeń dla studenta,
6. zwrot sprzętu w terminie,
7. zwrot sprzętu po terminie z naliczeniem kary,
8. wygenerowanie raportu końcowego.

Dzięki temu można od razu zobaczyć działanie najważniejszych elementów systemu bez tworzenia osobnego menu tekstowego.

---

## Możliwe dalsze rozwinięcia

Projekt można rozbudować o:
- menu tekstowe do ręcznej obsługi użytkownika,
- zapis i odczyt danych z plików JSON,
- integrację z bazą danych,
- walidację unikalności użytkowników i sprzętu,
- dodatkowe typy sprzętu,
- bardziej rozbudowane raporty,
- testy jednostkowe.

---

## Podsumowanie

Projekt realizuje wymagania aplikacji konsolowej do obsługi uczelnianej wypożyczalni sprzętu z użyciem podejścia obiektowego.
Najważniejszy nacisk został położony na:
- czytelny model domenowy,
- rozdzielenie odpowiedzialności,
- logikę biznesową poza `Program.cs`,
- przejrzystą strukturę projektu.
