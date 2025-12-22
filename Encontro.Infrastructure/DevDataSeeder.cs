using Encontro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Encontro.Infrastructure;

public class DevDataSeeder
{
    private readonly AppDbContext _context;

    public DevDataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedTestEventParticipantsAsync()
    {
        // Check if test event already exists
        var testEventName = "ECC 2024 - Teste Ordenação Completo";
        var existingEvent = await _context.Events
            .FirstOrDefaultAsync(e => e.Name == testEventName);

        if (existingEvent != null)
        {
            Console.WriteLine("Test data already exists. Skipping seed.");
            return;
        }

        Console.WriteLine("Seeding comprehensive test data for development environment...");

        // Create test event
        var testEvent = new Event
        {
            Name = testEventName,
            EventType = EventType.ECC,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _context.Events.Add(testEvent);
        await _context.SaveChangesAsync();

        // Create 65 test people with Brazilian names
        var people = new List<Person>
        {
            // Names starting with A-E (for alphabetical testing)
            new Person { Name = "Amanda Oliveira" },
            new Person { Name = "André Santos" },
            new Person { Name = "Beatriz Silva" },
            new Person { Name = "Bruno Costa" },
            new Person { Name = "Camila Rodrigues" },
            new Person { Name = "Carlos Mendes" },
            new Person { Name = "Diana Rocha" },
            new Person { Name = "Daniel Ferreira" },
            new Person { Name = "Eduardo Lima" },
            new Person { Name = "Eliane Martins" },
            // Names starting with F-J
            new Person { Name = "Fernanda Santos" },
            new Person { Name = "Felipe Almeida" },
            new Person { Name = "Gabriela Souza" },
            new Person { Name = "Gustavo Pereira" },
            new Person { Name = "Helena Carvalho" },
            new Person { Name = "Hugo Nascimento" },
            new Person { Name = "Isabela Ribeiro" },
            new Person { Name = "Igor Castro" },
            new Person { Name = "Juliana Dias" },
            new Person { Name = "João Araújo" },
            // Names starting with K-O
            new Person { Name = "Karla Gomes" },
            new Person { Name = "Lucas Barbosa" },
            new Person { Name = "Larissa Monteiro" },
            new Person { Name = "Marcelo Cardoso" },
            new Person { Name = "Mariana Freitas" },
            new Person { Name = "Nicolas Teixeira" },
            new Person { Name = "Natália Correia" },
            new Person { Name = "Otávio Moreira" },
            new Person { Name = "Olivia Azevedo" },
            new Person { Name = "Paulo Barros" },
            // Names starting with P-T
            new Person { Name = "Patrícia Cunha" },
            new Person { Name = "Rafael Moura" },
            new Person { Name = "Renata Lopes" },
            new Person { Name = "Roberto Ramos" },
            new Person { Name = "Sabrina Vieira" },
            new Person { Name = "Sérgio Farias" },
            new Person { Name = "Tatiana Medeiros" },
            new Person { Name = "Thiago Nunes" },
            new Person { Name = "Vanessa Campos" },
            new Person { Name = "Vítor Duarte" },
            // Additional names for complete coverage
            new Person { Name = "Wellington Pinto" },
            new Person { Name = "Yara Borges" },
            new Person { Name = "Alice Macedo" },
            new Person { Name = "Arthur Soares" },
            new Person { Name = "Bianca Rezende" },
            new Person { Name = "Caio Mendonça" },
            new Person { Name = "Débora Cavalcanti" },
            new Person { Name = "Erick Nogueira" },
            new Person { Name = "Fábio Xavier" },
            new Person { Name = "Giovana Santana" },
            new Person { Name = "Henrique Batista" },
            new Person { Name = "Íris Magalhães" },
            new Person { Name = "Júlio Tavares" },
            new Person { Name = "Kátia Melo" },
            new Person { Name = "Leonardo Siqueira" },
            new Person { Name = "Lúcia Guimarães" },
            new Person { Name = "Márcio Amaral" },
            new Person { Name = "Melissa Pires" },
            new Person { Name = "Nilton Braga" },
            new Person { Name = "Priscila Fonseca" },
            new Person { Name = "Rodrigo Pacheco" },
            new Person { Name = "Sílvia Matos" },
            new Person { Name = "Túlio Andrade" },
            new Person { Name = "Valéria Moraes" },
            new Person { Name = "Wilson Torres" }
        };

        _context.People.AddRange(people);
        await _context.SaveChangesAsync();

        // Get all teams and roles
        var teams = await _context.Teams.OrderBy(t => t.Order).ToListAsync();
        var roles = await _context.Roles.OrderBy(r => r.Order).ToListAsync();

        var participants = new List<EventParticipant>();
        var personIndex = 0;
        var baseDate = DateTime.Parse("2024-01-01 08:00:00");

        // 1. NULL team, NULL role scenarios (5 people)
        for (int i = 0; i < 5; i++)
        {
            participants.Add(new EventParticipant
            {
                EventId = testEvent.Id,
                PersonId = people[personIndex++].Id,
                TeamId = null,
                RoleId = null,
                RegisteredAt = baseDate.AddHours(i),
                Stage = (i % 10) + 1,
                Notes = $"NULL/NULL - Teste {i + 1}"
            });
        }

        // 2. NULL team, WITH role scenarios (5 people - using first 5 roles)
        for (int i = 0; i < 5; i++)
        {
            participants.Add(new EventParticipant
            {
                EventId = testEvent.Id,
                PersonId = people[personIndex++].Id,
                TeamId = null,
                RoleId = roles[i].Id,
                RegisteredAt = baseDate.AddDays(1).AddHours(i),
                Stage = (i % 10) + 1,
                Notes = $"NULL/{roles[i].Name}"
            });
        }

        // 3. WITH team, NULL role scenarios (5 people - using first 5 teams)
        for (int i = 0; i < 5; i++)
        {
            participants.Add(new EventParticipant
            {
                EventId = testEvent.Id,
                PersonId = people[personIndex++].Id,
                TeamId = teams[i].Id,
                RoleId = null,
                RegisteredAt = baseDate.AddDays(2).AddHours(i),
                Stage = (i % 10) + 1,
                Notes = $"{teams[i].Name}/NULL"
            });
        }

        // 4. Complete coverage: 2 people per team with different roles (42 people)
        for (int teamIdx = 0; teamIdx < teams.Count; teamIdx++)
        {
            for (int p = 0; p < 2; p++)
            {
                if (personIndex >= people.Count) break;
                
                var roleIdx = (teamIdx * 2 + p) % roles.Count;
                participants.Add(new EventParticipant
                {
                    EventId = testEvent.Id,
                    PersonId = people[personIndex++].Id,
                    TeamId = teams[teamIdx].Id,
                    RoleId = roles[roleIdx].Id,
                    RegisteredAt = baseDate.AddDays(3 + teamIdx).AddHours(p * 2),
                    Stage = ((teamIdx + p) % 10) + 1,
                    Notes = $"{teams[teamIdx].Order}-{teams[teamIdx].Name}/{roles[roleIdx].Order}-{roles[roleIdx].Name}"
                });
            }
        }

        // 5. Same team, same role, different names (3 people - Seguimista/Coordenador)
        var seguimista = teams.FirstOrDefault(t => t.Order == "00");
        var coordenador = roles.FirstOrDefault(r => r.Order == "12");
        if (seguimista != null && coordenador != null && personIndex + 2 < people.Count)
        {
            for (int i = 0; i < 3; i++)
            {
                participants.Add(new EventParticipant
                {
                    EventId = testEvent.Id,
                    PersonId = people[personIndex++].Id,
                    TeamId = seguimista.Id,
                    RoleId = coordenador.Id,
                    RegisteredAt = baseDate.AddDays(30).AddHours(i),
                    Stage = 5,
                    Notes = "Same team/role - test name sorting"
                });
            }
        }

        _context.EventParticipants.AddRange(participants);
        await _context.SaveChangesAsync();

        Console.WriteLine($"✅ Complete test data seeded successfully!");
        Console.WriteLine($"   Created {people.Count} people and {participants.Count} participants");
        Console.WriteLine($"   Coverage: {teams.Count} teams, {roles.Count} roles");
        Console.WriteLine($"   NULL scenarios: 15 participants");
        Console.WriteLine($"   Full coverage: {participants.Count - 15} participants with team/role");
        Console.WriteLine("");
        Console.WriteLine("Sort order validation:");
        Console.WriteLine("1️⃣  NULL team, NULL role (5 people) - sorted by name");
        Console.WriteLine("2️⃣  NULL team, with role (5 people) - sorted by role order");
        Console.WriteLine("3️⃣  With team, NULL role (5 people) - sorted by team order");
        Console.WriteLine("4️⃣  All teams covered (21 teams x 2 people) - sorted by team/role/name");
        Console.WriteLine("5️⃣  Same team/role (3 people) - sorted alphabetically");
    }
}
