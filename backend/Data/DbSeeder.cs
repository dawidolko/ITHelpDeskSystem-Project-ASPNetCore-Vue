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

        var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
        var techPasswordHash = BCrypt.Net.BCrypt.HashPassword("Tech123!");
        var userPasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!");

        var users = new List<User>
        {
            new User { FirstName = "Admin", LastName = "Kowalski", Email = "admin@firma.pl", PasswordHash = passwordHash, PhoneNumber = "+48 123 001", Role = "Admin", Department = "IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-12) },
            new User { FirstName = "Admin", LastName = "Nowak", Email = "admin1@firma.pl", PasswordHash = passwordHash, PhoneNumber = "+48 123 002", Role = "Admin", Department = "IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-11) },
            new User { FirstName = "Admin", LastName = "Wiśniewski", Email = "admin2@firma.pl", PasswordHash = passwordHash, PhoneNumber = "+48 123 003", Role = "Admin", Department = "IT", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-10) },
            
            new User { FirstName = "Tech", LastName = "Lewandowski", Email = "tech@firma.pl", PasswordHash = techPasswordHash, PhoneNumber = "+48 123 010", Role = "Technician", Department = "Wsparcie", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-9) },
            new User { FirstName = "Tech", LastName = "Wójcik", Email = "tech1@firma.pl", PasswordHash = techPasswordHash, PhoneNumber = "+48 123 011", Role = "Technician", Department = "Wsparcie", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-8) },
            new User { FirstName = "Tech", LastName = "Kamiński", Email = "tech2@firma.pl", PasswordHash = techPasswordHash, PhoneNumber = "+48 123 012", Role = "Technician", Department = "Sieć", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-7) },
            new User { FirstName = "Tech", LastName = "Zieliński", Email = "tech3@firma.pl", PasswordHash = techPasswordHash, PhoneNumber = "+48 123 013", Role = "Technician", Department = "Wsparcie", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) },
            new User { FirstName = "Tech", LastName = "Szymański", Email = "tech4@firma.pl", PasswordHash = techPasswordHash, PhoneNumber = "+48 123 014", Role = "Technician", Department = "Hardware", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-5) },
            
            new User { FirstName = "Jan", LastName = "Kowalczyk", Email = "user@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 100", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-4) },
            new User { FirstName = "Anna", LastName = "Kamińska", Email = "user1@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 101", Role = "User", Department = "Marketing", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-4) },
            new User { FirstName = "Piotr", LastName = "Dąbrowski", Email = "user2@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 102", Role = "User", Department = "Finanse", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-3) },
            new User { FirstName = "Katarzyna", LastName = "Pawlak", Email = "user3@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 103", Role = "User", Department = "HR", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-3) },
            new User { FirstName = "Michał", LastName = "Górski", Email = "user4@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 104", Role = "User", Department = "Operacje", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-2) },
            new User { FirstName = "Ewa", LastName = "Rutkowska", Email = "user5@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 105", Role = "User", Department = "Logistyka", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-2) },
            new User { FirstName = "Tomasz", LastName = "Sikora", Email = "user6@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 106", Role = "User", Department = "Sprzedaż", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-1) },
            new User { FirstName = "Magdalena", LastName = "Adamczyk", Email = "user7@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 107", Role = "User", Department = "Marketing", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-1) },
            new User { FirstName = "Andrzej", LastName = "Kowalski", Email = "user8@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 108", Role = "User", Department = "Finanse", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-20) },
            new User { FirstName = "Barbara", LastName = "Mazur", Email = "user9@firma.pl", PasswordHash = userPasswordHash, PhoneNumber = "+48 123 109", Role = "User", Department = "HR", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-10) }
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
            ("Awaria serwera produkcyjnego", "Główny serwer produkcyjny nie odpowiada. Brak dostępu do systemu.", TicketStatus.InProgress, TicketPriority.Critical, TicketCategory.Hardware),
            ("Brak połączenia z bazą danych", "Nie można połączyć się z główną bazą danych.", TicketStatus.InProgress, TicketPriority.Critical, TicketCategory.Network),
            ("System email nie działa", "Dział sprzedaży nie może wysyłać ani otrzymywać emaili.", TicketStatus.Open, TicketPriority.High, TicketCategory.Email),
            ("VPN ciągle się rozłącza", "Pracownicy zdalni doświadczają ciągłych rozłączeń VPN.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Network),
            ("Drukarka nie działa", "Drukarka w sali konferencyjnej pokazuje błąd.", TicketStatus.Resolved, TicketPriority.Medium, TicketCategory.Hardware),
            ("Nie można zainstalować oprogramowania", "Podczas instalacji Adobe pojawia się błąd.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Software),
            ("Wolny internet w biurze", "Prędkość internetu jest bardzo wolna.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Network),
            ("Prośba o reset hasła", "Potrzeba zresetować hasło do konta.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account),
            ("Laptop nie ładuje baterii", "Bateria laptopa nie ładuje się.", TicketStatus.Open, TicketPriority.Medium, TicketCategory.Hardware),
            ("Monitor mruga", "Monitor losowo mruga.", TicketStatus.New, TicketPriority.Medium, TicketCategory.Hardware),
            ("Prośba o nową klawiaturę", "Klawiatura ma lepiące się klawisze.", TicketStatus.New, TicketPriority.Low, TicketCategory.Hardware),
            ("Aktualizacja Office", "Potrzebuję aktualizacji do Office 365.", TicketStatus.New, TicketPriority.Low, TicketCategory.Software),
            ("Prośba o dostęp do dysku", "Potrzebuję dostępu do dysku Marketing.", TicketStatus.Closed, TicketPriority.Low, TicketCategory.Account),
            ("Mysz podwójnie klika", "Mysz rejestruje podwójne kliknięcie.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Komputer nie uruchamia się", "Komputer nie włącza się wcale.", TicketStatus.Open, TicketPriority.Critical, TicketCategory.Hardware),
            ("Program księgowy błędny", "System księgowy pokazuje błędy.", TicketStatus.Open, TicketPriority.High, TicketCategory.Software),
            ("Głośniki nie działają", "Głośniki nie wydają dźwięku.", TicketStatus.Open, TicketPriority.Low, TicketCategory.Hardware),
            ("Excel się zawiesza", "Excel zamraża się przy dużych plikach.", TicketStatus.InProgress, TicketPriority.Medium, TicketCategory.Software),
            ("Teams się rozłącza", "Teams rozłącza połączenia video.", TicketStatus.InProgress, TicketPriority.High, TicketCategory.Software),
            ("Prośba o dostęp do CRM", "Nowy pracownik potrzebuje dostępu do CRM.", TicketStatus.Closed, TicketPriority.Medium, TicketCategory.Account)
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
            "Rozwiązanie zostało wdrożone. Proszę o weryfikację.",
            "Przekazuję zgłoszenie do specjalisty.",
            "Naprawa w toku. Szacowany czas: 2 godziny.",
            "Wymieniono uszkodzony komponent. Proszę przetestować.",
            "Problem wynikał z błędnej konfiguracji. Poprawiono."
        };

        for (int i = 0; i < Math.Min(15, tickets.Count); i++)
        {
            var ticket = tickets[i];
            var numComments = random.Next(1, 3);
            
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
