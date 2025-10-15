# 🔧 PRZYKŁADY ZAPYTAŃ API (cURL)

## Testowanie walidacji parametrów

### ❌ Test 1: Nieprawidłowy numer strony (ujemny)

```bash
curl -X GET "http://localhost:5000/api/tickets?page=-5" -H "accept: application/json"
```

**Oczekiwany rezultat:** 400 Bad Request

```json
{
  "message": "Page number must be at least 1",
  "parameter": "Page",
  "value": -5
}
```

---

### ❌ Test 2: Zbyt duży rozmiar strony

```bash
curl -X GET "http://localhost:5000/api/tickets?pageSize=999" -H "accept: application/json"
```

**Oczekiwany rezultat:** 400 Bad Request

```json
{
  "message": "PageSize must be between 1 and 100",
  "parameter": "PageSize",
  "value": 999
}
```

---

### ❌ Test 3: Nieistniejący ID użytkownika (assignedToId)

```bash
curl -X GET "http://localhost:5000/api/tickets?assignedToId=999999" -H "accept: application/json"
```

**Oczekiwany rezultat:** 400 Bad Request

```json
{
  "message": "Assigned user not found",
  "parameter": "AssignedToId",
  "value": 999999
}
```

---

### ❌ Test 4: Nieistniejący ID użytkownika (createdById)

```bash
curl -X GET "http://localhost:5000/api/tickets?createdById=999999" -H "accept: application/json"
```

**Oczekiwany rezultat:** 400 Bad Request

```json
{
  "message": "Creator user not found",
  "parameter": "CreatedById",
  "value": 999999
}
```

---

### ❌ Test 5: Przekroczenie liczby stron

```bash
curl -X GET "http://localhost:5000/api/tickets?page=1000" -H "accept: application/json"
```

**Oczekiwany rezultat:** 400 Bad Request

```json
{
  "message": "Page number 1000 exceeds total pages (13)",
  "parameter": "Page",
  "value": 1000,
  "totalPages": 13,
  "totalCount": 125
}
```

---

### ❌ Test 6: Nieprawidłowe pole sortowania

```bash
curl -X GET "http://localhost:5000/api/tickets?sortBy=invalid" -H "accept: application/json"
```

**Oczekiwany rezultat:** 400 Bad Request

```json
{
  "message": "Invalid query parameters",
  "errors": {
    "SortBy": [
      "SortBy must be one of: id, title, status, priority, category, createdAt, updatedAt, viewcount"
    ]
  }
}
```

---

### ❌ Test 7: Nieprawidłowy kierunek sortowania

```bash
curl -X GET "http://localhost:5000/api/tickets?sortOrder=invalid" -H "accept: application/json"
```

**Oczekiwany rezultat:** 400 Bad Request

```json
{
  "message": "Invalid query parameters",
  "errors": {
    "SortOrder": ["SortOrder must be 'asc' or 'desc'"]
  }
}
```

---

## ✅ Prawidłowe zapytania

### Test 8: Podstawowe zapytanie

```bash
curl -X GET "http://localhost:5000/api/tickets" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK z listą zgłoszeń

---

### Test 9: Paginacja

```bash
curl -X GET "http://localhost:5000/api/tickets?page=1&pageSize=10" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK z 10 zgłoszeniami na pierwszej stronie

---

### Test 10: Filtrowanie po statusie

```bash
curl -X GET "http://localhost:5000/api/tickets?status=Open" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK z otwartymi zgłoszeniami

---

### Test 11: Filtrowanie po priorytecie

```bash
curl -X GET "http://localhost:5000/api/tickets?priority=High" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK z wysokim priorytetem

---

### Test 12: Filtrowanie po kategorii

```bash
curl -X GET "http://localhost:5000/api/tickets?category=Hardware" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK ze zgłoszeniami sprzętowymi

---

### Test 13: Wyszukiwanie

```bash
curl -X GET "http://localhost:5000/api/tickets?search=printer" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK ze zgłoszeniami zawierającymi "printer"

---

### Test 14: Sortowanie

```bash
curl -X GET "http://localhost:5000/api/tickets?sortBy=priority&sortOrder=desc" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK posortowane po priorytecie malejąco

---

### Test 15: Kombinacja wszystkich parametrów

```bash
curl -X GET "http://localhost:5000/api/tickets?page=1&pageSize=10&status=Open&priority=High&category=Hardware&search=printer&sortBy=createdAt&sortOrder=desc" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK z filtrowanymi i posortowanymi zgłoszeniami

---

### Test 16: Filtrowanie przeterminowanych

```bash
curl -X GET "http://localhost:5000/api/tickets?isOverdue=true" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK z przeterminowanymi zgłoszeniami

---

### Test 17: Filtrowanie po przypisanym technikowi (istniejący)

```bash
curl -X GET "http://localhost:5000/api/tickets?assignedToId=5" -H "accept: application/json"
```

**Oczekiwany rezultat:** 200 OK ze zgłoszeniami przypisanymi do technika ID=5

---

## 📊 Inne endpointy

### Pobierz szczegóły zgłoszenia

```bash
curl -X GET "http://localhost:5000/api/tickets/1" -H "accept: application/json"
```

---

### Statystyki

```bash
curl -X GET "http://localhost:5000/api/tickets/statistics" -H "accept: application/json"
```

---

### Lista użytkowników

```bash
curl -X GET "http://localhost:5000/api/users" -H "accept: application/json"
```

---

### Lista techników

```bash
curl -X GET "http://localhost:5000/api/users/technicians" -H "accept: application/json"
```

---

### Szczegóły użytkownika

```bash
curl -X GET "http://localhost:5000/api/users/1" -H "accept: application/json"
```

---

## 🎯 Szybki test walidacji (wszystkie testy na raz)

```bash
#!/bin/bash

echo "🧪 TESTOWANIE WALIDACJI API"
echo ""

echo "Test 1: Nieprawidłowy page..."
curl -s -X GET "http://localhost:5000/api/tickets?page=-5" | grep -q "Page number must be at least 1" && echo "✅ PASS" || echo "❌ FAIL"

echo "Test 2: Zbyt duży pageSize..."
curl -s -X GET "http://localhost:5000/api/tickets?pageSize=999" | grep -q "PageSize must be between 1 and 100" && echo "✅ PASS" || echo "❌ FAIL"

echo "Test 3: Nieistniejący assignedToId..."
curl -s -X GET "http://localhost:5000/api/tickets?assignedToId=999999" | grep -q "Assigned user not found" && echo "✅ PASS" || echo "❌ FAIL"

echo "Test 4: Nieistniejący createdById..."
curl -s -X GET "http://localhost:5000/api/tickets?createdById=999999" | grep -q "Creator user not found" && echo "✅ PASS" || echo "❌ FAIL"

echo "Test 5: Przekroczenie liczby stron..."
curl -s -X GET "http://localhost:5000/api/tickets?page=1000" | grep -q "exceeds total pages" && echo "✅ PASS" || echo "❌ FAIL"

echo "Test 6: Nieprawidłowy sortBy..."
curl -s -X GET "http://localhost:5000/api/tickets?sortBy=invalid" | grep -q "SortBy must be one of" && echo "✅ PASS" || echo "❌ FAIL"

echo "Test 7: Nieprawidłowy sortOrder..."
curl -s -X GET "http://localhost:5000/api/tickets?sortOrder=invalid" | grep -q "SortOrder must be" && echo "✅ PASS" || echo "❌ FAIL"

echo ""
echo "✅ Wszystkie testy zakończone!"
```

Zapisz jako `test_validation.sh` i uruchom:

```bash
chmod +x test_validation.sh
./test_validation.sh
```

---

## 📝 Notatki

- Wszystkie testy walidacji powinny zwracać **400 Bad Request**
- Jeśli otrzymujesz **200 OK** z pustą listą - walidacja NIE działa!
- Backend musi być uruchomiony: `cd backend && dotnet run`
- Swagger UI: http://localhost:5000/swagger
