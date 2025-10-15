# 🧪 INSTRUKCJA TESTOWANIA WALIDACJI

## Jak sprawdzić, że paginacja i walidacja działają poprawnie

### 1. Uruchom Backend

```bash
cd backend
dotnet run
```

Poczekaj aż zobaczysz:

```
Now listening on: http://localhost:5000
```

### 2. Otwórz Swagger UI

Wejdź na: **http://localhost:5000/swagger**

---

## 🔍 TESTY WALIDACJI - KROK PO KROKU

### Test 1: Nieprawidłowy numer strony (ujemny)

1. W Swagger UI rozwiń **`GET /api/tickets`**
2. Kliknij **"Try it out"**
3. W polu `page` wpisz: **`-5`**
4. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- Response body:

```json
{
  "message": "Page number must be at least 1",
  "parameter": "Page",
  "value": -5
}
```

**❌ JEŚLI ZWRACA 200 OK** - walidacja NIE działa!

---

### Test 2: Zbyt duży rozmiar strony

1. W Swagger UI w **`GET /api/tickets`**
2. Wyczyść wszystkie pola
3. W polu `pageSize` wpisz: **`999`**
4. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- Response body:

```json
{
  "message": "PageSize must be between 1 and 100",
  "parameter": "PageSize",
  "value": 999
}
```

---

### Test 3: Nieistniejący ID użytkownika (assignedToId)

1. W Swagger UI w **`GET /api/tickets`**
2. Wyczyść wszystkie pola
3. W polu `assignedToId` wpisz: **`999999`**
4. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- Response body:

```json
{
  "message": "Assigned user not found",
  "parameter": "AssignedToId",
  "value": 999999
}
```

**❌ JEŚLI ZWRACA 200 OK Z PUSTĄ LISTĄ** - walidacja NIE działa!

---

### Test 4: Nieistniejący ID użytkownika (createdById)

1. W Swagger UI w **`GET /api/tickets`**
2. Wyczyść wszystkie pola
3. W polu `createdById` wpisz: **`999999`**
4. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- Response body:

```json
{
  "message": "Creator user not found",
  "parameter": "CreatedById",
  "value": 999999
}
```

---

### Test 5: Przekroczenie liczby stron

1. W Swagger UI w **`GET /api/tickets`**
2. Wyczyść wszystkie pola
3. W polu `page` wpisz: **`1000`**
4. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- Response body:

```json
{
  "message": "Page number 1000 exceeds total pages (X)",
  "parameter": "Page",
  "value": 1000,
  "totalPages": 13,
  "totalCount": 125
}
```

---

### Test 6: Nieprawidłowe pole sortowania

1. W Swagger UI w **`GET /api/tickets`**
2. Wyczyść wszystkie pola
3. W polu `sortBy` wpisz: **`invalid`**
4. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- Response body:

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

### Test 7: Nieprawidłowy kierunek sortowania

1. W Swagger UI w **`GET /api/tickets`**
2. Wyczyść wszystkie pola
3. W polu `sortOrder` wpisz: **`invalid`**
4. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- Response body:

```json
{
  "message": "Invalid query parameters",
  "errors": {
    "SortOrder": ["SortOrder must be 'asc' or 'desc'"]
  }
}
```

---

### Test 8: Prawidłowe zapytanie (wszystko OK)

1. W Swagger UI w **`GET /api/tickets`**
2. Ustaw parametry:
   - `page`: **`1`**
   - `pageSize`: **`10`**
   - `status`: **`Open`**
   - `priority`: **`High`**
   - `sortBy`: **`createdAt`**
   - `sortOrder`: **`desc`**
3. Kliknij **"Execute"**

**✅ OCZEKIWANY REZULTAT:**

- Status: `200 OK`
- Response body:

```json
{
  "items": [...],
  "totalCount": 25,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 3,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

---

## 🌐 TESTY W PRZEGLĄDARCE (FRONTEND)

### 1. Uruchom Frontend

```bash
cd frontend
npm run dev
```

### 2. Otwórz aplikację

Wejdź na: **http://localhost:5173/tickets**

### 3. Test w Developer Tools (F12)

#### Test A: Zmiana pageSize w inspektorze

1. Otwórz Developer Tools (F12)
2. Przejdź do zakładki **Network**
3. Na stronie zmień stronę (kliknij "Next")
4. W Network znajdź zapytanie do `/api/tickets`
5. Kliknij prawym na zapytanie → **Edit and Resend**
6. Zmień `pageSize=10` na `pageSize=999`
7. Kliknij **Send**

**✅ OCZEKIWANY REZULTAT:**

- Status: `400 Bad Request`
- W Console powinien być błąd: "PageSize must be between 1 and 100"

#### Test B: Zmiana assignedToId w URL

1. W przeglądarce zmień URL na:
   ```
   http://localhost:5173/tickets?assignedToId=999999
   ```
2. Naciśnij Enter

**✅ OCZEKIWANY REZULTAT:**

- W Network powinno być zapytanie z `assignedToId=999999`
- Status: `400 Bad Request`
- Na stronie powinien wyświetlić się komunikat o błędzie

---

## 📋 CHECKLIST TESTÓW

Zaznacz każdy test po wykonaniu:

- [ ] Test 1: Ujemny numer strony → 400 Bad Request ✅
- [ ] Test 2: PageSize > 100 → 400 Bad Request ✅
- [ ] Test 3: Nieistniejący assignedToId → 400 Bad Request ✅
- [ ] Test 4: Nieistniejący createdById → 400 Bad Request ✅
- [ ] Test 5: Page > totalPages → 400 Bad Request ✅
- [ ] Test 6: Nieprawidłowy sortBy → 400 Bad Request ✅
- [ ] Test 7: Nieprawidłowy sortOrder → 400 Bad Request ✅
- [ ] Test 8: Prawidłowe zapytanie → 200 OK ✅
- [ ] Test A: Zmiana pageSize w inspektorze → 400 ✅
- [ ] Test B: Zmiana ID w URL → 400 ✅

---

## 🎯 PODSUMOWANIE

**Jeśli wszystkie testy przeszły pomyślnie, oznacza to:**

✅ Paginacja działa PO STRONIE BACKENDU  
✅ Walidacja parametrów działa poprawnie  
✅ Nieistniejące ID są wykrywane  
✅ Nieprawidłowe wartości nie są auto-korygowane  
✅ Backend zwraca odpowiednie błędy 400 Bad Request  
✅ Frontend otrzymuje i wyświetla błędy walidacji

---

## 🐛 CO JEŚLI TEST NIE PRZECHODZI?

### Jeśli otrzymujesz 200 OK zamiast 400:

1. Sprawdź czy backend jest uruchomiony z najnowszym kodem
2. Zrestartuj backend: `Ctrl+C` i `dotnet run`
3. Wyczyść cache przeglądarki
4. Sprawdź logi backendu w terminalu

### Jeśli Swagger nie działa:

1. Sprawdź czy backend działa na: http://localhost:5000
2. Sprawdź URL Swagger: http://localhost:5000/swagger (nie /swagger/index.html)
3. Sprawdź logi w terminalu backendu

### Jeśli Frontend nie wyświetla błędów:

1. Otwórz Developer Tools (F12)
2. Sprawdź zakładkę **Console** - powinny być błędy
3. Sprawdź zakładkę **Network** - powinno być 400 Bad Request

---

**POWODZENIA W TESTOWANIU!** 🚀
