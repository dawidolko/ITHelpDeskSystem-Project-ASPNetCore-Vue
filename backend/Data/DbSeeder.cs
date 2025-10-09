using HelpDeskAPI.Models;

namespace HelpDeskAPI.Data;

public static class DbSeeder
{
    public static void Initialize(HelpDeskContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        var users = new List<User>
        {
            new User { FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@firma.pl", PhoneNumber = "+48 123 456 001", Role = "Admin", Department = "Administracja IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-24) },
            new User { FirstName = "Krystyna", LastName = "Adamczyk", Email = "krystyna.adamczyk@firma.pl", PhoneNumber = "+48 123 456 002", Role = "Admin", Department = "Administracja IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-22) },
            new User { FirstName = "Zbigniew", LastName = "Mazur", Email = "zbigniew.mazur@firma.pl", PhoneNumber = "+48 123 456 003", Role = "Admin", Department = "Bezpieczeństwo IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-20) },
            new User { FirstName = "Elżbieta", LastName = "Krawczyk", Email = "elzbieta.krawczyk@firma.pl", PhoneNumber = "+48 123 456 004", Role = "Admin", Department = "Administracja IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-18) },
            new User { FirstName = "Stanisław", LastName = "Piotrowski", Email = "stanislaw.piotrowski@firma.pl", PhoneNumber = "+48 123 456 005", Role = "Admin", Department = "Infrastruktura IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-16) },
            
            new User { FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@firma.pl", PhoneNumber = "+48 123 456 010", Role = "Technician", Department = "Wsparcie IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-15) },
            new User { FirstName = "Piotr", LastName = "Wiśniewski", Email = "piotr.wisniewski@firma.pl", PhoneNumber = "+48 123 456 011", Role = "Technician", Department = "Wsparcie IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-14) },
            new User { FirstName = "Katarzyna", LastName = "Wójcik", Email = "katarzyna.wojcik@firma.pl", PhoneNumber = "+48 123 456 012", Role = "Technician", Department = "Zespół Sieciowy", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-13) },
            new User { FirstName = "Michał", LastName = "Kamiński", Email = "michal.kaminski@firma.pl", PhoneNumber = "+48 123 456 013", Role = "Technician", Department = "Wsparcie IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-12) },
            new User { FirstName = "Barbara", LastName = "Lewandowska", Email = "barbara.lewandowska@firma.pl", PhoneNumber = "+48 123 456 014", Role = "Technician", Department = "Zespół Sieciowy", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-11) },
            new User { FirstName = "Rafał", LastName = "Zieliński", Email = "rafal.zielinski@firma.pl", PhoneNumber = "+48 123 456 015", Role = "Technician", Department = "Wsparcie Sprzętowe", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-10) },
            new User { FirstName = "Monika", LastName = "Szymańska", Email = "monika.szymanska@firma.pl", PhoneNumber = "+48 123 456 016", Role = "Technician", Department = "Wsparcie Oprogramowania", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-9) },
            new User { FirstName = "Grzegorz", LastName = "Woźniak", Email = "grzegorz.wozniak@firma.pl", PhoneNumber = "+48 123 456 017", Role = "Technician", Department = "Zespół Sieciowy", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-8) },
            new User { FirstName = "Joanna", LastName = "Dąbrowska", Email = "joanna.dabrowska@firma.pl", PhoneNumber = "+48 123 456 018", Role = "Technician", Department = "Wsparcie IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-7) },
            new User { FirstName = "Andrzej", LastName = "Kozłowski", Email = "andrzej.kozlowski@firma.pl", PhoneNumber = "+48 123 456 019", Role = "Technician", Department = "Wsparcie Sprzętowe", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) },
            
            new User { FirstName = "Marek", LastName = "Kowalczyk", Email = "marek.kowalczyk@firma.pl", PhoneNumber = "+48 123 456 100", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-5) },
            new User { FirstName = "Agnieszka", LastName = "Kamińska", Email = "agnieszka.kaminska@firma.pl", PhoneNumber = "+48 123 456 101", Role = "User", Department = "Marketing", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-5) },
            new User { FirstName = "Tomasz", LastName = "Lewandowski", Email = "tomasz.lewandowski@firma.pl", PhoneNumber = "+48 123 456 102", Role = "User", Department = "Finanse", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-4) },
            new User { FirstName = "Magdalena", LastName = "Zielińska", Email = "magdalena.zielinska@firma.pl", PhoneNumber = "+48 123 456 103", Role = "User", Department = "HR", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-4) },
            new User { FirstName = "Krzysztof", LastName = "Szymański", Email = "krzysztof.szymanski@firma.pl", PhoneNumber = "+48 123 456 104", Role = "User", Department = "Operacje", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-3) },
            new User { FirstName = "Ewa", LastName = "Jankowska", Email = "ewa.jankowska@firma.pl", PhoneNumber = "+48 123 456 105", Role = "User", Department = "Prawo", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-3) },
            new User { FirstName = "Paweł", LastName = "Wojciechowski", Email = "pawel.wojciechowski@firma.pl", PhoneNumber = "+48 123 456 106", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-2) },
            new User { FirstName = "Małgorzata", LastName = "Kwiatkowska", Email = "malgorzata.kwiatkowska@firma.pl", PhoneNumber = "+48 123 456 107", Role = "User", Department = "Marketing", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-2) },
            new User { FirstName = "Jacek", LastName = "Kaczmarek", Email = "jacek.kaczmarek@firma.pl", PhoneNumber = "+48 123 456 108", Role = "User", Department = "Finanse", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-2) },
            new User { FirstName = "Dorota", LastName = "Piotrowska", Email = "dorota.piotrowska@firma.pl", PhoneNumber = "+48 123 456 109", Role = "User", Department = "HR", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-1) },
            new User { FirstName = "Marcin", LastName = "Grabowski", Email = "marcin.grabowski@firma.pl", PhoneNumber = "+48 123 456 110", Role = "User", Department = "Operacje", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-1) },
            new User { FirstName = "Beata", LastName = "Pawlak", Email = "beata.pawlak@firma.pl", PhoneNumber = "+48 123 456 111", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-1) },
            new User { FirstName = "Dariusz", LastName = "Michalski", Email = "dariusz.michalski@firma.pl", PhoneNumber = "+48 123 456 112", Role = "User", Department = "Logistyka", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-25) },
            new User { FirstName = "Iwona", LastName = "Król", Email = "iwona.krol@firma.pl", PhoneNumber = "+48 123 456 113", Role = "User", Department = "Księgowość", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-20) },
            new User { FirstName = "Robert", LastName = "Sikora", Email = "robert.sikora@firma.pl", PhoneNumber = "+48 123 456 114", Role = "User", Department = "Produkcja", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-18) },
            new User { FirstName = "Sylwia", LastName = "Baran", Email = "sylwia.baran@firma.pl", PhoneNumber = "+48 123 456 115", Role = "User", Department = "Jakość", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-15) },
            new User { FirstName = "Adam", LastName = "Wróbel", Email = "adam.wrobel@firma.pl", PhoneNumber = "+48 123 456 116", Role = "User", Department = "R&D", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-12) },
            new User { FirstName = "Karolina", LastName = "Jaworska", Email = "karolina.jaworska@firma.pl", PhoneNumber = "+48 123 456 117", Role = "User", Department = "Marketing", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-10) },
            new User { FirstName = "Łukasz", LastName = "Mazurek", Email = "lukasz.mazurek@firma.pl", PhoneNumber = "+48 123 456 118", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-8) },
            new User { FirstName = "Natalia", LastName = "Kucharska", Email = "natalia.kucharska@firma.pl", PhoneNumber = "+48 123 456 119", Role = "User", Department = "HR", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-5) },
            new User { FirstName = "Damian", LastName = "Walczak", Email = "damian.walczak@firma.pl", PhoneNumber = "+48 123 456 120", Role = "User", Department = "Logistyka", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new User { FirstName = "Patrycja", LastName = "Kubiak", Email = "patrycja.kubiak@firma.pl", PhoneNumber = "+48 123 456 121", Role = "User", Department = "Finanse", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-2) },
            new User { FirstName = "Sebastian", LastName = "Rutkowski", Email = "sebastian.rutkowski@firma.pl", PhoneNumber = "+48 123 456 122", Role = "User", Department = "Produkcja", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-1) },
            new User { FirstName = "Weronika", LastName = "Borkowska", Email = "weronika.borkowska@firma.pl", PhoneNumber = "+48 123 456 123", Role = "User", Department = "Jakość", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-1) },
            new User { FirstName = "Filip", LastName = "Krajewski", Email = "filip.krajewski@firma.pl", PhoneNumber = "+48 123 456 124", Role = "User", Department = "R&D", IsActive = true, CreatedAt = DateTime.UtcNow.AddHours(-12) },
            new User { FirstName = "Aleksandra", LastName = "Baranowska", Email = "aleksandra.baranowska@firma.pl", PhoneNumber = "+48 123 456 125", Role = "User", Department = "Marketing", IsActive = true, CreatedAt = DateTime.UtcNow.AddHours(-8) },
            new User { FirstName = "Kamil", LastName = "Kalinowski", Email = "kamil.kalinowski@firma.pl", PhoneNumber = "+48 123 456 126", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow.AddHours(-6) },
            new User { FirstName = "Paulina", LastName = "Sobczak", Email = "paulina.sobczak@firma.pl", PhoneNumber = "+48 123 456 127", Role = "User", Department = "Operacje", IsActive = true, CreatedAt = DateTime.UtcNow.AddHours(-4) },
            new User { FirstName = "Bartosz", LastName = "Głowacki", Email = "bartosz.glowacki@firma.pl", PhoneNumber = "+48 123 456 128", Role = "User", Department = "Księgowość", IsActive = true, CreatedAt = DateTime.UtcNow.AddHours(-2) },
            new User { FirstName = "Zuzanna", LastName = "Sawicki", Email = "zuzanna.sawicki@firma.pl", PhoneNumber = "+48 123 456 129", Role = "User", Department = "HR", IsActive = true, CreatedAt = DateTime.UtcNow.AddHours(-1) },
            new User { FirstName = "Maciej", LastName = "Maciejewski", Email = "maciej.maciejewski@firma.pl", PhoneNumber = "+48 123 456 130", Role = "User", Department = "Logistyka", IsActive = true, CreatedAt = DateTime.UtcNow },
            new User { FirstName = "Julia", LastName = "Pawłowska", Email = "julia.pawlowska@firma.pl", PhoneNumber = "+48 123 456 131", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow },
            new User { FirstName = "Wojciech", LastName = "Witkowski", Email = "wojciech.witkowski@firma.pl", PhoneNumber = "+48 123 456 132", Role = "User", Department = "Produkcja", IsActive = true, CreatedAt = DateTime.UtcNow },
            new User { FirstName = "Oliwia", LastName = "Górska", Email = "oliwia.gorska@firma.pl", PhoneNumber = "+48 123 456 133", Role = "User", Department = "Jakość", IsActive = true, CreatedAt = DateTime.UtcNow },
            new User { FirstName = "Kacper", LastName = "Wieczorek", Email = "kacper.wieczorek@firma.pl", PhoneNumber = "+48 123 456 134", Role = "User", Department = "R&D", IsActive = true, CreatedAt = DateTime.UtcNow }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        var admins = users.Where(u => u.Role == "Admin").ToList();
        var technicians = users.Where(u => u.Role == "Technician").ToList();
        var regularUsers = users.Where(u => u.Role == "User").ToList();

        var random = new Random(42);
        var tickets = new List<Ticket>();

        var polishTickets = new[] 
        {
            ("Awaria serwera produkcyjnego", "Główny serwer produkcyjny nie odpowiada. Brak dostępu do systemu dla wszystkich użytkowników. To wpływa krytycznie na operacje biznesowe.", TicketStatus.InProgress, TicketPriority.Critical, TicketCategory.Hardware),
            ("Brak połączenia z bazą danych", "Nie można połączyć się z główną bazą danych. Wszystkie aplikacje zgłaszają błędy połączenia.", TicketStatus.InProgress, TicketPriority.Critical, TicketCategory.Network),
            ("Przestój systemu backupu", "System tworzenia kopii zapasowych nie działa od wczoraj. Konieczna natychmiastowa naprawa.", TicketStatus.Open, TicketPriority.Critical, TicketCategory.Software),
            ("Atak DDoS na serwer", "Wykryto atak DDoS na główny serwer. Strona firmowa jest niedostępna.", TicketStatus.InProgress, TicketPriority.Critical, TicketCategory.Network),
            ("Wyciek danych - pilne", "Podejrzenie wycieku danych klientów. Wymaga natychmiastowej interwencji zespołu bezpieczeństwa.", TicketStatus.InProgress, TicketPriority.Critical, TicketCategory.Other),
            
            ("System email nie działa dla całego działu", "Dział sprzedaży nie może wysyłać ani otrzymywać emaili. Blokuje to komunikację z klientami.", TicketStatus.Open, TicketPriority.High, TicketCategory.Email),
            ("VPN ciągle się rozłącza", "Pracownicy zdalni doświadczają ciągłych rozłączeń VPN. Znacząco wpływa to na produktywność.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Network),
            ("Awaria systemu backupu", "Nocny automatyczny backup zakończył się niepowodzeniem. Wymaga pilnej naprawy.", TicketStatus.Open, TicketPriority.High, TicketCategory.Software),
            ("Wolne działanie sieci w biurze", "Prędkość internetu znacząco spadła. Testy pokazują tylko 10% normalnej przepustowości.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Network),
            ("Brak dostępu do serwera plików", "Nie można się połączyć z głównym serwerem plików. Wielu użytkowników zgłasza problem.", TicketStatus.Open, TicketPriority.High, TicketCategory.Network),
            ("Awaria drukarki w sali konferencyjnej A", "Drukarka pokazuje błąd 'paper jam' mimo braku zacięcia. Wielokrotnie próbowano zrestartować.", TicketStatus.Resolved, TicketPriority.High, TicketCategory.Hardware),
            ("System CRM nie odpowiada", "Aplikacja CRM przestała działać. Dział sprzedaży nie ma dostępu do danych klientów.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Software),
            ("Problemy z serwerem aplikacji", "Serwer aplikacji wyrzuca błędy 500. Część funkcjonalności jest niedostępna.", TicketStatus.Open, TicketPriority.High, TicketCategory.Software),
            ("Brak dostępu do panelu administracyjnego", "Nie można zalogować się do panelu admina. Problem dotyczy całego zespołu IT.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Account),
            ("Antywirusowanie wykryło zagrożenie", "System antywirusowy wykrył potencjalne zagrożenie na serwerze. Wymaga pilnej weryfikacji.", TicketStatus.Open, TicketPriority.High, TicketCategory.Software),
            
            ("Drukarka nie działa w sali konferencyjnej B", "Drukarka w sali B pokazuje błąd. Próby ponownego uruchomienia nie pomogły.", TicketStatus.Resolved, TicketPriority.Medium, TicketCategory.Hardware),
            ("Nie można zainstalować oprogramowania", "Podczas próby instalacji Adobe Acrobat Pro pojawia się błąd 'access denied'.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Wolny internet w biurze", "Prędkość internetu jest znacznie wolniejsza od kilku dni.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Network),
            ("Prośba o reset hasła do konta serwisowego", "Potrzeba zresetować hasło do współdzielonego konta używanego przez dział finansów.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Laptop nie ładuje baterii", "Bateria laptopa nie ładuje się nawet po podłączeniu. Laptop wyłącza się po odłączeniu zasilacza.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Monitor mruga losowo", "Monitor podłączony przez HDMI losowo mruga. Wypróbowano różne kable z tym samym efektem.", TicketStatus.New, TicketPriority.Medium, TicketCategory.Hardware),
            ("Nie można zamapować dysku sieciowego", "Podczas próby mapowania dysku Z: pojawia się błąd 'network path not found'.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Network),
            ("Outlook ciągle prosi o hasło", "Outlook wymaga podania hasła co 30 minut. Próbowano zapisać poświadczenia.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Email),
            ("WiFi rozłącza się na laptopie", "Połączenie WiFi na laptopie rozłącza się co 10-15 minut. Ethernet działa poprawnie.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Network),
            ("Kamera nie działa w Teams", "Kamera działa w innych aplikacjach, ale pokazuje czarny ekran w Microsoft Teams.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Hardware),
            ("Antywirus blokuje legalną aplikację", "Nowe oprogramowanie CRM jest blokowane przez antywirus. Potrzebny wyjątek.", TicketStatus.Resolved, TicketPriority.Medium, TicketCategory.Software),
            ("Brak dostępu do współdzielonego kalendarza", "Podczas próby wyświetlenia współdzielonego kalendarza zespołu pojawia się błąd 'permission denied'.", TicketStatus.New, TicketPriority.Medium, TicketCategory.Email),
            ("Błąd przy wysyłaniu dużych plików mailem", "Nie można wysłać emaili z załącznikami większymi niż 10MB. Wcześniej działało.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Email),
            ("Problem z dostępem do dysku współdzielonego", "Nie mam dostępu do folderu Marketing na dysku współdzielonym.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Wolne działanie komputera", "Komputer działa bardzo wolno od kilku dni. Uruchamianie aplikacji trwa wieki.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            
            ("Prośba o nową klawiaturę", "Klawiatura ma lepiące się klawisze. Proszę o wymianę w dogodnym terminie.", TicketStatus.New, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o aktualizację Microsoft Office", "Nadal używam Office 2019. Chciałbym zaktualizować do Office 365.", TicketStatus.New, TicketPriority.Low, TicketCategory.Software),
            ("Stojak monitora wymaga regulacji", "Stojak monitora jest luźny i monitor ciągle się pochyla. Potrzebna pomoc przy dokręceniu.", TicketStatus.Resolved, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o dostęp do dysku współdzielonego", "Potrzebuję dostępu do dysku Marketing dla materiałów nowej kampanii.", TicketStatus.Closed, TicketPriority.Low, TicketCategory.Account),
            ("Zniknęły ikony z pulpitu", "Wszystkie ikony z pulpitu zniknęły po ostatniej aktualizacji Windows. Mogę uruchamiać programy przez menu Start.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Software),
            ("Mysz podwójnie klika", "Mysz rejestruje podwójne kliknięcie przy pojedynczym kliknięciu. Bardzo frustrujące podczas pracy.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o instalację Python IDE", "Potrzebuję zainstalowanego PyCharm do analizy danych. Manager działu zatwierdził.", TicketStatus.Closed, TicketPriority.Low, TicketCategory.Software),
            ("Ekran przechyla się", "Monitor przechyla się na boki. Stojak wymaga dokręcenia śrub.", TicketStatus.Resolved, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o nową mysz bezprzewodową", "Mysz przewodowa jest niewygodna. Proszę o wymianę na bezprzewodową.", TicketStatus.New, TicketPriority.Low, TicketCategory.Hardware),
            ("Popękane słuchawki", "Słuchawki są popękane przy pałąku. Nadal działają, ale proszę o wymianę.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            
            ("Komputer nie uruchamia się", "Komputer nie włącza się. Po naciśnięciu przycisku power nic się nie dzieje.", TicketStatus.Open, TicketPriority.Critical, TicketCategory.Hardware),
            ("Utrata danych po awarii", "Po wczorajszej awarii nie mogę znaleźć plików z projektu. Proszę o pomoc w odzyskaniu.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Other),
            ("Brak połączenia z drukarką sieciową", "Nie mogę drukować na drukarce sieciowej. Inne komputery drukują bez problemu.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Network),
            ("Ekran się zamraża", "Ekran komputera losowo się zamraża. Trzeba restart komputera, żeby znowu działał.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Hardware),
            ("Program księgowy wyrzuca błędy", "System księgowy pokazuje błąd przy próbie generowania raportów.", TicketStatus.Open, TicketPriority.High, TicketCategory.Software),
            ("Głośniki nie działają", "Głośniki komputera nie wydają dźwięku. Sprawdzono ustawienia - wszystko OK.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Nie można uzyskać dostępu przez VPN", "VPN pokazuje błąd 'Authentication failed' mimo poprawnego hasła.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Network),
            ("Skan nie działa na drukarce", "Funkcja skanowania na drukarce wielofunkcyjnej nie działa. Drukowanie jest OK.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Excel ciągle się zawiesza", "Excel zamraża się przy otwieraniu dużych plików. Problem jest systematyczny.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Software),
            ("Prośba o zwiększenie limitu emaila", "Osiągam limit miejsca na skrzynce pocztowej. Proszę o zwiększenie.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Email),
            
            ("Niebieskie ekrany przy starcie", "BSOD z kodem błędu 0x0000007B pojawia się co kilka uruchomień. Czasem trzeba 3-4 restartów.", TicketStatus.Open, TicketPriority.High, TicketCategory.Hardware),
            ("Teams rozłącza się podczas spotkań", "Microsoft Teams rozłącza się w trakcie video spotkań. Problem systematyczny.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Software),
            ("Prośba o dostęp do systemu CRM", "Nowy pracownik w dziale sprzedaży potrzebuje dostępu do CRM.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Hasło wygasło - nie mogę się zalogować", "Hasło do systemu wygasło i nie mogę go zmienić przez stronę.", TicketStatus.Resolved, TicketPriority.High, TicketCategory.Account),
            ("Prośba o instalację dodatkowego monitora", "Potrzebuję drugiego monitora do pracy z arkuszami. Manager zatwierdził.", TicketStatus.New, TicketPriority.Low, TicketCategory.Hardware),
            ("Długi czas ładowania aplikacji", "Aplikacja do zarządzania magazynem ładuje się 10+ minut. Wcześniej była szybka.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Problemy z połączeniem Bluetooth", "Nie mogę połączyć słuchawek Bluetooth z laptopem. Inne urządzenia działają.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o instalację Zoom", "Potrzebuję Zoom do spotkań z klientami zewnętrznymi.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Software),
            ("Brak dźwięku w video konferencjach", "Podczas spotkań Teams słyszę innych, ale oni mnie nie słyszą.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Hardware),
            ("Prośba o dostęp do repozytorium GitHub", "Potrzebuję dostępu do firmowego repozytorium GitHub dla nowego projektu.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Account),
            
            ("Komputer nadmiernie się grzeje", "Laptop jest bardzo gorący podczas pracy. Wentylator pracuje na maksymalnych obrotach.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Prośba o aktualizację przeglądarki", "Używam starej wersji Chrome. Niektóre strony nie działają poprawnie.", TicketStatus.Closed, TicketPriority.Low, TicketCategory.Software),
            ("System Windows wymaga aktywacji", "Pojawił się komunikat 'Windows wymaga aktywacji'. Proszę o pomoc.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Prośba o konfigurację podpisu email", "Potrzebuję skonfigurować firmowy podpis w emailu zgodnie ze standardem.", TicketStatus.Resolved, TicketPriority.Low, TicketCategory.Email),
            ("Nie działa skaner kodów kreskowych", "Skaner kodów kreskowych w magazynie nie skanuje. Sprawdzono podłączenie - OK.", TicketStatus.Open, TicketPriority.High, TicketCategory.Hardware),
            ("Prośba o szkolenie z nowego systemu", "Potrzebuję szkolenia z nowego systemu HR. Nie wiem jak z niego korzystać.", TicketStatus.New, TicketPriority.Low, TicketCategory.Other),
            ("Problem z synchronizacją OneDrive", "OneDrive nie synchronizuje plików. Pokazuje błąd synchronizacji.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Software),
            ("Brak dostępu do folderu zespołu", "Nie widzę folderu Teams dla naszego projektu. Inni członkowie zespołu mają dostęp.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Account),
            ("Prośba o upgrade RAM", "Komputer ma za mało pamięci RAM. Często się zawiesza przy wielu otwartych programach.", TicketStatus.New, TicketPriority.Medium, TicketCategory.Hardware),
            ("Komunikat o pełnym dysku C", "Dysk C: jest pełny. System pokazuje ostrzeżenie o braku miejsca.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            
            ("Prośba o dostęp do bazy testowej", "Developer potrzebuje dostępu do bazy testowej dla nowego projektu.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Nieprawidłowe wyświetlanie strony", "Strona firmowa wyświetla się nieprawidłowo w przeglądarce Safari.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Software),
            ("Problem z logowaniem do systemu płac", "Nie mogę się zalogować do systemu płacowego. Hasło jest poprawne.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Account),
            ("Prośba o odblokowanie strony", "Potrzebuję dostępu do LinkedIn dla celów rekrutacyjnych. Strona jest zablokowana.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Account),
            ("Wolne kopiowanie plików na serwer", "Kopiowanie plików na serwer trwa bardzo długo. Transfery po 1-2 MB/s.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Network),
            ("Port USB nie działa", "Jeden z portów USB w laptopie nie rozpoznaje urządzeń. Inne porty działają.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o instalację oprogramowania CAD", "Inżynier potrzebuje AutoCAD do pracy nad projektami.", TicketStatus.New, TicketPriority.Medium, TicketCategory.Software),
            ("Problemy z drukowaniem kolorowym", "Drukarka drukuje tylko czarno-białe mimo ustawienia kolorowego wydruku.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Czcionki wyglądają rozmazane", "Czcionki na monitorze są rozmazane i nieostre. Trudno czytać tekst.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o dostęp do API produkcyjnego", "Developer potrzebuje kluczy API do środowiska produkcyjnego.", TicketStatus.Closed, TicketPriority.High, TicketCategory.Account),
            
            ("Laptop nie łączy się z projektorami", "Laptop nie wykrywa projektorów w salach konferencyjnych. Inne laptopy działają.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Hardware),
            ("Prośba o zmianę uprawnień folderów", "Potrzebuję uprawnień do edycji w folderze projektów. Obecnie mam tylko odczyt.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Account),
            ("Problem z certyfikatem SSL", "Przeglądarka pokazuje ostrzeżenie o nieprawidłowym certyfikacie SSL dla serwisu wewnętrznego.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Network),
            ("Prośba o konto FTP", "Potrzebuję konta FTP do przesyłania plików na serwer webowy.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Automatyczne wylogowanie z systemu", "System wylogowuje mnie automatycznie co 15 minut. Bardzo utrudnia pracę.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Brak możliwości zmiany rozdzielczości", "Nie mogę zmienić rozdzielczości monitora. Opcja jest wyszarzona.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Software),
            ("Prośba o licencję Adobe Creative Cloud", "Grafik potrzebuje licencji Adobe CC do pracy nad projektami marketingowymi.", TicketStatus.New, TicketPriority.Medium, TicketCategory.Software),
            ("Problem z mikrofonem w laptopie", "Mikrofon w laptopie nie działa. Sprawdzono ustawienia - mikrofon jest włączony.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Wiadomości trafiają do spamu", "Firmowe emaile trafiają do spamu u odbiorców. Problem jest systematyczny.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Email),
            ("Prośba o dostęp do systemu raportowania", "Manager potrzebuje dostępu do systemu BI do generowania raportów.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Account),
            
            ("Błąd podczas zapisu pliku", "Pojawia się błąd 'access denied' przy próbie zapisania pliku na dysku sieciowym.", TicketStatus.Open, TicketPriority.High, TicketCategory.Network),
            ("Touchpad nie działa poprawnie", "Touchpad w laptopie zachowuje się chaotycznie. Kursor skacze losowo.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Prośba o instalację narzędzi developer", "Potrzebuję zainstalowanego Visual Studio Code i Node.js dla nowego projektu.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Software),
            ("Problem z rozpoznawaniem drukarki", "System nie widzi drukarki sieciowej. Próbowano ponownej instalacji driverów.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Network),
            ("Prośba o większy dysk w laptopie", "Dysk twardy jest pełny. Potrzebuję większego dysku lub SSD.", TicketStatus.New, TicketPriority.Low, TicketCategory.Hardware),
            ("Aplikacja mobilna nie synchronizuje", "Aplikacja firmowa na telefonie nie synchronizuje się z serwerem.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Prośba o dostęp do dashboard Analytics", "Marketing manager potrzebuje dostępu do Google Analytics dashboard.", TicketStatus.Closed, TicketPriority.Low, TicketCategory.Account),
            ("Problem z klawiaturą bezprzewodową", "Klawiatura bezprzewodowa ciągle się rozłącza. Wymieniono baterie - bez efektu.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Backup zajmuje zbyt wiele czasu", "Backup danych trwa ponad 6 godzin. Wcześniej trwał 2 godziny.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Software),
            ("Prośba o zmianę hasła administratora", "Hasło do panelu admin zostało skompromitowane. Wymaga pilnej zmiany.", TicketStatus.Resolved, TicketPriority.Critical, TicketCategory.Account),
            
            ("Nie działa automatyczne przekierowanie email", "Reguła przekierowania emaili przestała działać. Wiadomości nie są przesyłane.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Email),
            ("Komputer włącza się bardzo wolno", "Uruchamianie komputera trwa ponad 10 minut. Problem narastał przez ostatni tydzień.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Prośba o instalację drukarki fiskalnej", "Księgowość potrzebuje skonfigurowanej drukarki fiskalnej.", TicketStatus.New, TicketPriority.High, TicketCategory.Hardware),
            ("Problem z zapisywaniem w programie", "Program księgowy nie pozwala zapisać zmian. Brak komunikatów o błędach.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Software),
            ("Telefon VoIP ciągle się rozłącza", "Telefon VoIP rozłącza połączenia po kilku minutach rozmowy.", TicketStatus.Open, TicketPriority.High, TicketCategory.Network),
            ("Prośba o upgrade systemu operacyjnego", "Nadal używam Windows 10. Chciałbym upgrade do Windows 11.", TicketStatus.New, TicketPriority.Low, TicketCategory.Software),
            ("Nie można usunąć plików z dysku", "Podczas próby usunięcia plików pojawia się komunikat 'file in use'. Żaden program nie jest otwarty.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Software),
            ("Prośba o dostęp do systemu zamówień", "Nowy pracownik magazynu potrzebuje dostępu do systemu zarządzania zamówieniami.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Monitor pokazuje artefakty", "Na ekranie monitora pojawiają się dziwne kolorowe linie i plamki.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Prośba o konfigurację dwuskładnikowego logowania", "Chcę włączyć 2FA dla mojego konta firmowego dla większego bezpieczeństwa.", TicketStatus.Resolved, TicketPriority.Low, TicketCategory.Account),
            
            ("Błąd przy aktualizacji systemu", "Windows Update wyrzuca błąd 0x80070003. Nie mogę zaktualizować systemu.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Klawiatura numeryczna nie działa", "Numpad w klawiaturze nie reaguje. Pozostałe klawisze działają normalnie.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o dostęp do systemu ERP", "Nowy pracownik w produkcji potrzebuje dostępu do systemu ERP.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Problem z kopiowaniem między monitorami", "Nie mogę przeciągać okien między dwoma monitorami. Kursor się zatrzymuje.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Software),
            ("Niska jakość dźwięku w słuchawkach", "Dźwięk w słuchawkach jest zniekształcony i trzeszczy.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Prośba o rozszerzenie godzin dostępu VPN", "Potrzebuję dostępu VPN również w weekendy dla pilnego projektu.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Account),
            ("Błąd certyfikatu w przeglądarce", "Przeglądarka blokuje dostęp do wewnętrznej aplikacji z powodu wygasłego certyfikatu.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Network),
            ("Program graficzny się zawiesza", "Adobe Illustrator zawiesza się przy zapisywaniu dużych plików.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Software),
            ("Prośba o dodanie drukarki do systemu", "Nowa drukarka w dziale nie jest widoczna w systemie. Potrzeba konfiguracji.", TicketStatus.New, TicketPriority.Medium, TicketCategory.Hardware),
            ("Wolne pobieranie plików z serwera", "Pobieranie plików z serwera zajmuje bardzo dużo czasu. Upload działa normalnie.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Network)
        };

        for (int i = 0; i < polishTickets.Length; i++)
        {
            var (title, desc, status, priority, category) = polishTickets[i];
            var user = regularUsers[random.Next(regularUsers.Count)];
            var tech = technicians[random.Next(technicians.Count)];
            
            var daysAgo = random.Next(0, 30);
            var hoursAgo = random.Next(0, 24);
            var createdAt = DateTime.UtcNow.AddDays(-daysAgo).AddHours(-hoursAgo);
            
            var ticket = new Ticket
            {
                Title = title,
                Description = desc,
                Status = status,
                Priority = priority,
                Category = category,
                CreatedById = user.Id,
                AssignedToId = (status == TicketStatus.New) ? null : tech.Id,
                CreatedAt = createdAt,
                UpdatedAt = createdAt.AddHours(random.Next(1, 12)),
                ViewCount = random.Next(1, 50)
            };

            if (status == TicketStatus.Resolved || status == TicketStatus.Closed)
            {
                ticket.ResolvedAt = ticket.UpdatedAt.AddHours(random.Next(1, 5));
                ticket.ResolutionNotes = "Problem został rozwiązany. System działa poprawnie.";
            }

            if (status == TicketStatus.Closed)
            {
                ticket.ClosedAt = ticket.ResolvedAt?.AddHours(random.Next(1, 3));
            }

            tickets.Add(ticket);
        }

        context.Tickets.AddRange(tickets);
        context.SaveChanges();

        var comments = new List<Comment>();
        var commentTemplates = new[]
        {
            "Sprawdzam problem. Zaraz wrócę z informacjami.",
            "Problem został zidentyfikowany. Pracuję nad rozwiązaniem.",
            "Potrzebuję więcej informacji. Czy możesz podać szczegóły?",
            "Rozwiązanie zostało wdrożone. Proszę o weryfikację.",
            "Przekazuję zgłoszenie do specjalisty.",
            "Problem wymaga interwencji zewnętrznego dostawcy.",
            "Zostanie rozwiązane w najbliższym czasie.",
            "Dziękuję za zgłoszenie. Zajmę się tym priorytetowo.",
            "Problem jest związany z konfiguracją. Poprawiam.",
            "Wymaga aktualizacji oprogramowania. Zaplanowano na jutro.",
            "Naprawa w toku. Szacowany czas: 2 godziny.",
            "Skontaktowałem się z dostawcą sprzętu.",
            "Wymieniono uszkodzony komponent. Proszę przetestować.",
            "Zainstalowano najnowsze aktualizacje sterowników.",
            "Problem wynikał z błędnej konfiguracji. Poprawiono.",
            "Zgłoszenie zostało eskalowane do wyższego poziomu wsparcia.",
            "Wykonano restart systemu. Problem powinien być rozwiązany.",
            "Konieczna wymiana sprzętu. Zamówiono nowy.",
            "Sprawdzono logi systemowe. Zidentyfikowano przyczynę.",
            "Przeprowadzono diagnostykę. Wymaga naprawy przez producenta."
        };

        for (int i = 0; i < Math.Min(80, tickets.Count); i++)
        {
            var ticket = tickets[i];
            var numComments = random.Next(1, 5);
            
            for (int j = 0; j < numComments; j++)
            {
                var commenter = (j % 2 == 0) ? technicians[random.Next(technicians.Count)] : regularUsers[random.Next(regularUsers.Count)];
                comments.Add(new Comment
                {
                    TicketId = ticket.Id,
                    AuthorId = commenter.Id,
                    Content = commentTemplates[random.Next(commentTemplates.Length)],
                    CreatedAt = ticket.CreatedAt.AddHours(j + 1),
                    IsInternal = (j % 3 == 0)
                });
            }
        }

        context.Comments.AddRange(comments);
        context.SaveChanges();

        Console.WriteLine($"✅ Baza danych została wypełniona danymi testowymi!");
        Console.WriteLine($"   📊 Użytkownicy: {users.Count}");
        Console.WriteLine($"   🎫 Zgłoszenia: {tickets.Count}");
        Console.WriteLine($"   💬 Komentarze: {comments.Count}");
    }
}
