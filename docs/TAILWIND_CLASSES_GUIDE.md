# 📐 Przewodnik po klasach Tailwind CSS w projekcie

## Spis treści

- [Spacing (Odstępy)](#spacing-odstępy)
- [Typography (Typografia)](#typography-typografia)
- [Colors (Kolory)](#colors-kolory)
- [Layout](#layout)
- [Flexbox & Grid](#flexbox--grid)
- [Sizing (Rozmiary)](#sizing-rozmiary)
- [Borders (Obramowania)](#borders-obramowania)
- [Effects (Efekty)](#effects-efekty)
- [Transitions & Animations](#transitions--animations)
- [Responsive Design](#responsive-design)

---

## Spacing (Odstępy)

Tailwind używa skali: **1 = 0.25rem = 4px**

### Padding (Wewnętrzne odstępy)

```css
p-1    /* padding: 0.25rem (4px) - wszystkie strony */
p-2    /* padding: 0.5rem (8px) */
p-3    /* padding: 0.75rem (12px) */
p-4    /* padding: 1rem (16px) */
p-6    /* padding: 1.5rem (24px) */
p-8    /* padding: 2rem (32px) */
p-9    /* padding: 2.25rem (36px) */
p-12   /* padding: 3rem (48px) */

/* Kierunkowe */
px-3   /* padding-left + padding-right: 0.75rem (12px) */
py-2   /* padding-top + padding-bottom: 0.5rem (8px) */
pt-6   /* padding-top: 1.5rem (24px) */
pb-4   /* padding-bottom: 1rem (16px) */
pl-4   /* padding-left: 1rem (16px) */
pr-4   /* padding-right: 1rem (16px) */
```

**Przykłady z projektu:**

```vue
<!-- Duży padding dla sekcji -->
<section class="py-20">  <!-- 80px góra/dół -->

<!-- Padding dla przycisków -->
<button class="px-8 py-4">  <!-- 32px lewo/prawo, 16px góra/dół -->

<!-- Padding dla kart -->
<div class="p-8">  <!-- 32px wszystkie strony -->
```

### Margin (Zewnętrzne odstępy)

```css
m-2    /* margin: 0.5rem (8px) */
m-4    /* margin: 1rem (16px) */
m-6    /* margin: 1.5rem (24px) */
m-8    /* margin: 2rem (32px) */

/* Kierunkowe */
mx-4   /* margin-left + margin-right: 1rem (16px) */
my-6   /* margin-top + margin-bottom: 1.5rem (24px) */
mt-2   /* margin-top: 0.5rem (8px) */
mb-4   /* margin-bottom: 1rem (16px) */
ml-2   /* margin-left: 0.5rem (8px) */
mr-2   /* margin-right: 0.5rem (8px) */

/* Auto centering */
mx-auto  /* margin: 0 auto - wycentrowanie poziome */
```

**Przykłady z projektu:**

```vue
<!-- Margines między elementami -->
<h1 class="mb-6">  <!-- 24px margin-bottom -->

<!-- Wycentrowanie kontenera -->
<div class="mx-auto">  <!-- wycentrowany poziomo -->

<!-- Odstęp między przyciskami -->
<button class="mr-2">  <!-- 8px margin-right -->
```

### Gap (Odstępy w Flexbox/Grid)

```css
gap-2   /* gap: 0.5rem (8px) */
gap-4   /* gap: 1rem (16px) */
gap-6   /* gap: 1.5rem (24px) */
gap-8   /* gap: 2rem (32px) */

/* Kierunkowe */
gap-x-4  /* column-gap: 1rem (16px) */
gap-y-6  /* row-gap: 1.5rem (24px) */
```

**Przykłady z projektu:**

```vue
<!-- Odstępy między przyciskami w flexbox -->
<div class="flex gap-4">  <!-- 16px między elementami -->

<!-- Odstępy w grid -->
<div class="grid gap-6">  <!-- 24px między elementami -->
```

---

## Typography (Typografia)

### Font Size (Rozmiar czcionki)

```css
text-xs    /* font-size: 0.75rem (12px), line-height: 1rem */
text-sm    /* font-size: 0.875rem (14px), line-height: 1.25rem */
text-base  /* font-size: 1rem (16px), line-height: 1.5rem */
text-lg    /* font-size: 1.125rem (18px), line-height: 1.75rem */
text-xl    /* font-size: 1.25rem (20px), line-height: 1.75rem */
text-2xl   /* font-size: 1.5rem (24px), line-height: 2rem */
text-3xl   /* font-size: 1.875rem (30px), line-height: 2.25rem */
text-4xl   /* font-size: 2.25rem (36px), line-height: 2.5rem */
text-5xl   /* font-size: 3rem (48px), line-height: 1 */
text-6xl   /* font-size: 3.75rem (60px), line-height: 1 */
text-7xl   /* font-size: 4.5rem (72px), line-height: 1 */
```

**Przykłady z projektu:**

```vue
<!-- Nagłówki -->
<h1 class="text-5xl">IT Help Desk</h1>
<!-- 48px -->
<h2 class="text-3xl">Dashboard</h2>
<!-- 30px -->

<!-- Treść -->
<p class="text-xl">Opis systemu</p>
<!-- 20px -->
<span class="text-sm">Mały tekst</span>
<!-- 14px -->

<!-- Badge -->
<span class="text-xs">CRITICAL</span>
<!-- 12px -->
```

### Font Weight (Grubość czcionki)

```css
font-thin        /* font-weight: 100 */
font-extralight  /* font-weight: 200 */
font-light       /* font-weight: 300 */
font-normal      /* font-weight: 400 */
font-medium      /* font-weight: 500 */
font-semibold    /* font-weight: 600 */
font-bold        /* font-weight: 700 */
font-extrabold   /* font-weight: 800 */
font-black       /* font-weight: 900 */
```

**Przykłady z projektu:**

```vue
<!-- Nagłówki -->
<h1 class="font-extrabold">Tytuł</h1>
<!-- 800 -->
<h2 class="font-bold">Podtytuł</h2>
<!-- 700 -->

<!-- Przyciski -->
<button class="font-semibold">Save</button>
<!-- 600 -->

<!-- Tekst zwykły -->
<p class="font-medium">Treść</p>
<!-- 500 -->
```

### Text Alignment (Wyrównanie tekstu)

```css
text-left     /* text-align: left */
text-center   /* text-align: center */
text-right    /* text-align: right */
text-justify  /* text-align: justify */
```

### Text Transform (Transformacje tekstu)

```css
uppercase   /* text-transform: uppercase - WIELKIE LITERY */
lowercase   /* text-transform: lowercase - małe litery */
capitalize  /* text-transform: capitalize - Pierwsza Wielka */
```

**Przykłady z projektu:**

```vue
<!-- Przyciski z wielkimi literami -->
<button class="uppercase">Create ticket</button>
<!-- CREATE TICKET -->

<!-- Nawigacja -->
<nav class="uppercase tracking-wide">Menu</nav>
<!-- MENU -->
```

### Letter Spacing (Odstępy między literami)

```css
tracking-tighter  /* letter-spacing: -0.05em */
tracking-tight    /* letter-spacing: -0.025em */
tracking-normal   /* letter-spacing: 0 */
tracking-wide     /* letter-spacing: 0.025em */
tracking-wider    /* letter-spacing: 0.05em */
tracking-widest   /* letter-spacing: 0.1em */
```

**Przykłady z projektu:**

```vue
<!-- Nagłówki z większymi odstępami -->
<h1 class="tracking-tight">Dashboard</h1>

<!-- Nawigacja -->
<nav class="tracking-widest">MENU</nav>
<!-- M E N U -->
```

### Line Height (Wysokość linii)

```css
leading-none    /* line-height: 1 */
leading-tight   /* line-height: 1.25 */
leading-snug    /* line-height: 1.375 */
leading-normal  /* line-height: 1.5 */
leading-relaxed /* line-height: 1.625 */
leading-loose   /* line-height: 2 */
```

---

## Colors (Kolory)

### Background Colors (Kolory tła)

```css
/* Czarny/Biały */
bg-black      /* background-color: #000000 */
bg-white      /* background-color: #ffffff */

/* Skala szarości (zinc) */
bg-zinc-50    /* background-color: #fafafa - bardzo jasny */
bg-zinc-100   /* background-color: #f4f4f5 */
bg-zinc-200   /* background-color: #e4e4e7 */
bg-zinc-300   /* background-color: #d4d4d8 */
bg-zinc-400   /* background-color: #a1a1aa */
bg-zinc-500   /* background-color: #71717a */
bg-zinc-600   /* background-color: #52525b */
bg-zinc-700   /* background-color: #3f3f46 */
bg-zinc-800   /* background-color: #27272a */
bg-zinc-900   /* background-color: #18181b - bardzo ciemny */

/* Skala szarości (gray) */
bg-gray-50    /* background-color: #f9fafb */
bg-gray-100   /* background-color: #f3f4f6 */
bg-gray-700   /* background-color: #374151 */
bg-gray-800   /* background-color: #1f2937 */
bg-gray-900   /* background-color: #111827 */

/* Kolory */
bg-red-500    /* background-color: #ef4444 */
bg-red-600    /* background-color: #dc2626 */
bg-green-500  /* background-color: #22c55e */
bg-green-600  /* background-color: #16a34a */
bg-blue-500   /* background-color: #3b82f6 */
bg-blue-600   /* background-color: #2563eb */

/* Custom color z config (k-main) */
bg-k-main     /* background-color: #9eff00 - zielony akcent */
```

**Opacity (Przezroczystość):**

```css
bg-red-500/10   /* background-color: rgba(239, 68, 68, 0.1) - 10% */
bg-red-500/20   /* background-color: rgba(239, 68, 68, 0.2) - 20% */
bg-red-500/30   /* background-color: rgba(239, 68, 68, 0.3) - 30% */
bg-red-500/50   /* background-color: rgba(239, 68, 68, 0.5) - 50% */
bg-red-500/80   /* background-color: rgba(239, 68, 68, 0.8) - 80% */
bg-red-500/90   /* background-color: rgba(239, 68, 68, 0.9) - 90% */
```

**Przykłady z projektu:**

```vue
<!-- Tło strony -->
<div class="bg-zinc-900">  <!-- Bardzo ciemne tło (#18181b) -->

<!-- Karty -->
<div class="bg-black">  <!-- Czarne karty (#000000) -->

<!-- Przyciski -->
<button class="bg-k-main">  <!-- Zielony przycisk (#9eff00) -->

<!-- Alert z przezroczystością -->
<div class="bg-red-500/10">  <!-- Czerwone tło z 10% opacity -->
```

### Text Colors (Kolory tekstu)

```css
text-white      /* color: #ffffff */
text-black      /* color: #000000 */

/* Szarości */
text-zinc-300   /* color: #d4d4d8 */
text-zinc-400   /* color: #a1a1aa */
text-zinc-500   /* color: #71717a */

text-gray-300   /* color: #d1d5db */
text-gray-400   /* color: #9ca3af */
text-gray-500   /* color: #6b7280 */

/* Kolory */
text-red-300    /* color: #fca5a5 */
text-red-400    /* color: #f87171 */
text-red-500    /* color: #ef4444 */
text-green-400  /* color: #4ade80 */
text-blue-400   /* color: #60a5fa */

/* Custom color */
text-k-main     /* color: #9eff00 */
```

**Przykłady z projektu:**

```vue
<!-- Tekst główny -->
<p class="text-white">Główny tekst</p>

<!-- Tekst drugorzędny -->
<span class="text-zinc-400">Pomocniczy tekst</span>

<!-- Badge statusu -->
<span class="text-red-400">Critical</span>
<span class="text-green-400">Resolved</span>

<!-- Akcent -->
<span class="text-k-main">IT HelpDesk</span>
```

### Border Colors (Kolory obramowania)

```css
border-zinc-700  /* border-color: #3f3f46 */
border-zinc-800  /* border-color: #27272a */
border-gray-700  /* border-color: #374151 */
border-red-500   /* border-color: #ef4444 */
border-k-main    /* border-color: #9eff00 */
```

---

## Layout

### Display

```css
block          /* display: block */
inline-block   /* display: inline-block */
inline         /* display: inline */
flex           /* display: flex */
inline-flex    /* display: inline-flex */
grid           /* display: grid */
hidden         /* display: none */
```

### Position

```css
static      /* position: static */
fixed       /* position: fixed */
absolute    /* position: absolute */
relative    /* position: relative */
sticky      /* position: sticky */
```

**Top/Right/Bottom/Left:**

```css
top-0      /* top: 0 */
right-0    /* right: 0 */
bottom-0   /* bottom: 0 */
left-0     /* left: 0 */

inset-0    /* top: 0; right: 0; bottom: 0; left: 0 */
```

### Z-Index (Warstwy)

```css
z-0     /* z-index: 0 */
z-10    /* z-index: 10 */
z-20    /* z-index: 20 */
z-30    /* z-index: 30 */
z-40    /* z-index: 40 */
z-50    /* z-index: 50 */
```

---

## Flexbox & Grid

### Flexbox

```css
/* Kierunek */
flex-row          /* flex-direction: row */
flex-row-reverse  /* flex-direction: row-reverse */
flex-col          /* flex-direction: column */
flex-col-reverse  /* flex-direction: column-reverse */

/* Wyrównanie główne (justify-content) */
justify-start     /* justify-content: flex-start */
justify-end       /* justify-content: flex-end */
justify-center    /* justify-content: center */
justify-between   /* justify-content: space-between */
justify-around    /* justify-content: space-around */

/* Wyrównanie krzyżowe (align-items) */
items-start       /* align-items: flex-start */
items-end         /* align-items: flex-end */
items-center      /* align-items: center */
items-baseline    /* align-items: baseline */
items-stretch     /* align-items: stretch */

/* Wrap */
flex-wrap         /* flex-wrap: wrap */
flex-nowrap       /* flex-wrap: nowrap */

/* Grow/Shrink */
flex-1            /* flex: 1 1 0% - zajmuje dostępne miejsce */
flex-auto         /* flex: 1 1 auto */
flex-none         /* flex: none */
```

**Przykłady z projektu:**

```vue
<!-- Wycentrowanie -->
<div class="flex items-center justify-center">
  Wycentrowane w obu osiach
</div>

<!-- Navbar z przestrzenią między -->
<nav class="flex justify-between items-center">
  <div>Logo</div>
  <div>Menu</div>
</nav>

<!-- Kolumna -->
<div class="flex flex-col gap-4">
  <div>Element 1</div>
  <div>Element 2</div>
</div>
```

### Grid

```css
/* Kolumny */
grid-cols-1   /* grid-template-columns: repeat(1, minmax(0, 1fr)) */
grid-cols-2   /* grid-template-columns: repeat(2, minmax(0, 1fr)) */
grid-cols-3   /* grid-template-columns: repeat(3, minmax(0, 1fr)) */
grid-cols-4   /* grid-template-columns: repeat(4, minmax(0, 1fr)) */

/* Span (rozciągnięcie) */
col-span-1    /* grid-column: span 1 / span 1 */
col-span-2    /* grid-column: span 2 / span 2 */
col-span-full /* grid-column: 1 / -1 */
```

**Przykłady z projektu:**

```vue
<!-- Grid 2 kolumny -->
<div class="grid grid-cols-2 gap-6">
  <div>Kolumna 1</div>
  <div>Kolumna 2</div>
</div>

<!-- Responsive grid -->
<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
  <div>Card 1</div>
  <div>Card 2</div>
  <div>Card 3</div>
</div>
```

---

## Sizing (Rozmiary)

### Width (Szerokość)

```css
w-full      /* width: 100% */
w-screen    /* width: 100vw */
w-auto      /* width: auto */
w-fit       /* width: fit-content */

/* Frakcje */
w-1/2       /* width: 50% */
w-1/3       /* width: 33.333333% */
w-2/3       /* width: 66.666667% */
w-1/4       /* width: 25% */
w-3/4       /* width: 75% */
w-4/5       /* width: 80% */

/* Stałe wartości */
w-64        /* width: 16rem (256px) */
w-96        /* width: 24rem (384px) */
```

**Max Width:**

```css
max-w-xs     /* max-width: 20rem (320px) */
max-w-sm     /* max-width: 24rem (384px) */
max-w-md     /* max-width: 28rem (448px) */
max-w-lg     /* max-width: 32rem (512px) */
max-w-xl     /* max-width: 36rem (576px) */
max-w-2xl    /* max-width: 42rem (672px) */
max-w-3xl    /* max-width: 48rem (768px) */
max-w-4xl    /* max-width: 56rem (896px) */
max-w-5xl    /* max-width: 64rem (1024px) */
max-w-6xl    /* max-width: 72rem (1152px) */
max-w-7xl    /* max-width: 80rem (1280px) */
```

**Przykłady z projektu:**

```vue
<!-- Kontener wycentrowany z max-width -->
<div class="w-4/5 max-w-6xl mx-auto">
  <!-- Szerokość 80%, max 1152px, wycentrowany -->
</div>

<!-- Pełna szerokość -->
<section class="w-full">
  <!-- 100% szerokości -->
</section>
```

### Height (Wysokość)

```css
h-full      /* height: 100% */
h-screen    /* height: 100vh */
h-auto      /* height: auto */

/* Stałe wartości */
h-64        /* height: 16rem (256px) */
h-96        /* height: 24rem (384px) */
```

**Min/Max Height:**

```css
min-h-screen  /* min-height: 100vh */
min-h-full    /* min-height: 100% */
max-h-screen  /* max-height: 100vh */
```

**Przykłady z projektu:**

```vue
<!-- Pełna wysokość ekranu -->
<div class="min-h-screen">
  <!-- Minimum 100vh -->
</div>

<!-- Pełna wysokość -->
<div class="h-full">
  <!-- 100% wysokości rodzica -->
</div>
```

---

## Borders (Obramowania)

### Border Width (Grubość)

```css
border       /* border-width: 1px */
border-0     /* border-width: 0 */
border-2     /* border-width: 2px */
border-4     /* border-width: 4px */
border-8     /* border-width: 8px */

/* Kierunkowe */
border-t     /* border-top-width: 1px */
border-b     /* border-bottom-width: 1px */
border-l     /* border-left-width: 1px */
border-r     /* border-right-width: 1px */
```

### Border Radius (Zaokrąglenie)

```css
rounded-none   /* border-radius: 0 */
rounded-sm     /* border-radius: 0.125rem (2px) */
rounded        /* border-radius: 0.25rem (4px) */
rounded-md     /* border-radius: 0.375rem (6px) */
rounded-lg     /* border-radius: 0.5rem (8px) */
rounded-xl     /* border-radius: 0.75rem (12px) */
rounded-2xl    /* border-radius: 1rem (16px) */
rounded-3xl    /* border-radius: 1.5rem (24px) */
rounded-full   /* border-radius: 9999px - pełne koło */
```

**Przykłady z projektu:**

```vue
<!-- Przycisk z zaokrągleniem -->
<button class="rounded-lg">  <!-- 8px zaokrąglenie -->
  Click me
</button>

<!-- Karta -->
<div class="rounded-xl">  <!-- 12px zaokrąglenie -->
  Card content
</div>

<!-- Badge okrągły -->
<span class="rounded-full">  <!-- Pełne koło -->
  99+
</span>
```

---

## Effects (Efekty)

### Shadow (Cień)

```css
shadow-none  /* box-shadow: none */
shadow-sm    /* box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.05) */
shadow       /* box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1) */
shadow-md    /* box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1) */
shadow-lg    /* box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1) */
shadow-xl    /* box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.1) */
shadow-2xl   /* box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25) */

/* Custom shadow */
shadow-k-main/50  /* Cień w kolorze k-main z 50% opacity */
```

**Przykłady z projektu:**

```vue
<!-- Karta z cieniem -->
<div class="shadow-2xl">  <!-- Duży cień -->
  Card
</div>

<!-- Przycisk z kolorowym cieniem przy hover -->
<button class="hover:shadow-k-main/50">
  Button
</button>
```

### Opacity (Przezroczystość)

```css
opacity-0     /* opacity: 0 - niewidoczne */
opacity-25    /* opacity: 0.25 */
opacity-50    /* opacity: 0.5 */
opacity-75    /* opacity: 0.75 */
opacity-100   /* opacity: 1 - pełna widoczność */
```

### Cursor

```css
cursor-auto       /* cursor: auto */
cursor-pointer    /* cursor: pointer - wskaźnik ręki */
cursor-not-allowed /* cursor: not-allowed - przekreślony */
```

---

## Transitions & Animations

### Transition (Przejścia)

```css
transition        /* transition: all 150ms cubic-bezier(0.4, 0, 0.2, 1) */
transition-all    /* transition-property: all */
transition-colors /* transition-property: color, background-color, border-color */
transition-opacity /* transition-property: opacity */
transition-transform /* transition-property: transform */

/* Duration (czas trwania) */
duration-75       /* transition-duration: 75ms */
duration-100      /* transition-duration: 100ms */
duration-150      /* transition-duration: 150ms */
duration-200      /* transition-duration: 200ms */
duration-300      /* transition-duration: 300ms */
duration-500      /* transition-duration: 500ms */
duration-700      /* transition-duration: 700ms */
duration-1000     /* transition-duration: 1000ms */
```

**Przykłady z projektu:**

```vue
<!-- Przycisk z smooth transition -->
<button class="transition duration-300 hover:scale-105">
  <!-- 300ms transition, powiększenie o 5% przy hover -->
</button>

<!-- Link z transition kolorów -->
<a class="transition-colors duration-300 hover:text-k-main">
  Link
</a>
```

### Transform

```css
/* Scale (skalowanie) */
scale-0      /* transform: scale(0) */
scale-50     /* transform: scale(0.5) */
scale-75     /* transform: scale(0.75) */
scale-90     /* transform: scale(0.9) */
scale-95     /* transform: scale(0.95) */
scale-100    /* transform: scale(1) */
scale-105    /* transform: scale(1.05) */
scale-110    /* transform: scale(1.1) */
scale-125    /* transform: scale(1.25) */
scale-150    /* transform: scale(1.5) */

/* Translate (przesunięcie) */
translate-x-0.5  /* transform: translateX(0.125rem) - 2px */
translate-y-0.5  /* transform: translateY(0.125rem) - 2px */

/* Rotate (obrót) */
rotate-0     /* transform: rotate(0deg) */
rotate-45    /* transform: rotate(45deg) */
rotate-90    /* transform: rotate(90deg) */
rotate-180   /* transform: rotate(180deg) */
```

**Przykłady z projektu:**

```vue
<!-- Przycisk z efektem naciśnięcia -->
<button class="active:translate-y-0.5">
  <!-- Przesunięcie o 2px w dół przy kliknięciu -->
</button>

<!-- Karta z zoom przy hover -->
<div class="hover:scale-110 transition">
  <!-- Powiększenie o 10% przy najechaniu -->
</div>
```

### Animations (Animacje)

```css
animate-spin     /* animation: spin 1s linear infinite - obracanie */
animate-ping     /* animation: ping 1s cubic-bezier(0, 0, 0.2, 1) infinite */
animate-pulse    /* animation: pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite */
animate-bounce   /* animation: bounce 1s infinite */
```

**Przykłady z projektu:**

```vue
<!-- Loader -->
<div class="animate-spin">
  Loading...
</div>

<!-- Pulsujący badge -->
<span class="animate-pulse">
  New
</span>
```

---

## Responsive Design

### Breakpoints (Punkty łamania)

```css
/* Domyślnie: mobile first (bez prefiksu) */
sm:   /* @media (min-width: 640px) - małe tablety */
md:   /* @media (min-width: 768px) - tablety */
lg:   /* @media (min-width: 1024px) - małe laptopy */
xl:   /* @media (min-width: 1280px) - laptopy */
2xl:  /* @media (min-width: 1536px) - duże ekrany */
```

**Przykłady z projektu:**

```vue
<!-- Responsywny grid -->
<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3">
  <!-- 1 kolumna na mobile, 2 na tablet, 3 na desktop -->
</div>

<!-- Responsywny tekst -->
<h1 class="text-4xl md:text-5xl lg:text-6xl">
  <!-- 36px na mobile, 48px na tablet, 60px na desktop -->
</h1>

<!-- Ukrywanie na mobile -->
<nav class="hidden lg:flex">
  <!-- Ukryte na mobile/tablet, widoczne od 1024px -->
</nav>

<!-- Szerokość responsywna -->
<div class="w-full md:w-11/12 lg:w-4/5">
  <!-- 100% na mobile, 91.67% na tablet, 80% na desktop -->
</div>
```

---

## Pseudo-classes (Stany)

### Hover (Najechanie myszką)

```css
hover:bg-blue-600   /* background-color zmienia się przy najechaniu */
hover:text-white    /* kolor tekstu zmienia się przy najechaniu */
hover:scale-105     /* powiększenie o 5% przy najechaniu */
hover:shadow-lg     /* cień pojawia się przy najechaniu */
```

### Focus (Fokus)

```css
focus:outline-none      /* usuwa domyślny outline */
focus:ring-2            /* dodaje ring o grubości 2px */
focus:ring-k-main       /* ring w kolorze k-main */
focus:border-k-main     /* border w kolorze k-main */
focus:border-transparent /* przezroczysty border */
```

**Przykłady z projektu:**

```vue
<!-- Input z focus state -->
<input
  class="focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent" />

<!-- Przycisk z hover effect -->
<button class="bg-k-main hover:bg-k-main/90 transition">
  Save
</button>
```

### Active (Kliknięcie)

```css
active:translate-y-0.5  /* przesunięcie o 2px w dół przy kliknięciu */
active:scale-95         /* zmniejszenie o 5% przy kliknięciu */
```

### Disabled (Wyłączony)

```css
disabled:opacity-50           /* 50% przezroczystości gdy disabled */
disabled:cursor-not-allowed   /* kursor "not-allowed" gdy disabled */
```

**Przykłady z projektu:**

```vue
<!-- Przycisk z disabled state -->
<button
  :disabled="loading"
  class="disabled:opacity-50 disabled:cursor-not-allowed">
  {{ loading ? 'Loading...' : 'Submit' }}
</button>
```

---

## Często używane kombinacje w projekcie

### 1. Kontener strony

```vue
<div class="min-h-screen w-screen bg-zinc-900 text-white">
  <!-- Pełna wysokość ekranu, pełna szerokość, ciemne tło, biały tekst -->
</div>
```

### 2. Kontener wycentrowany

```vue
<div class="w-4/5 max-w-6xl mx-auto md:w-11/12 lg:w-4/5">
  <!-- 80% szerokości (max 1152px), wycentrowany, responsive -->
</div>
```

### 3. Przycisk główny

```vue
<button
  class="rounded-lg bg-k-main px-8 py-4 text-lg font-bold uppercase tracking-wide text-black transition duration-300 hover:scale-105 active:translate-y-0.5">
  <!-- Zaokrąglony, zielone tło, padding, wielkie litery, czarny tekst,
       efekt powiększenia przy hover, przesunięcie przy kliknięciu -->
</button>
```

### 4. Przycisk drugorzędny

```vue
<button
  class="rounded-lg border-2 border-k-main px-8 py-4 text-lg font-bold uppercase tracking-wide text-k-main transition duration-300 hover:bg-k-main hover:text-black active:translate-y-0.5">
  <!-- Outline button, zmienia się w solid przy hover -->
</button>
```

### 5. Karta (Card)

```vue
<div class="rounded-xl bg-black p-8 shadow-2xl border border-zinc-800">
  <!-- Zaokrąglona, czarne tło, padding 32px, duży cień, border -->
</div>
```

### 6. Input field

```vue
<input class="w-full px-4 py-3 bg-gray-900 border border-gray-700 rounded-lg text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all">
  <!-- Pełna szerokość, padding, ciemne tło, focus state z zielonym ringiem -->
</input>
```

### 7. Badge statusu

```vue
<span
  class="inline-block rounded-full px-3 py-1 text-xs font-bold bg-red-500/20 text-red-400 border border-red-500">
  CRITICAL
  <!-- Okrągły badge, małe litery, czerwony z przezroczystością -->
</span>
```

### 8. Navigation link

```vue
<router-link
  class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5">
  Dashboard
  <!-- Wielkie litery, zmiana koloru przy hover, przesunięcie przy kliknięciu -->
</router-link>
```

### 9. Grid responsywny

```vue
<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
  <!-- 1 kolumna na mobile, 2 na tablet, 3 na desktop, 24px gap -->
</div>
```

### 10. Flexbox wycentrowany

```vue
<div class="flex items-center justify-center min-h-screen">
  <!-- Wycentrowanie w pionie i poziomie, pełna wysokość ekranu -->
</div>
```

---

## Custom colors z tailwind.config.js

```javascript
colors: {
  'k-main': '#9eff00',  // Zielony akcent używany w projekcie
  'k-black': '#000000'  // Czarny
}
```

**Użycie:**

```vue
<div class="bg-k-main text-black">  <!-- Zielone tło, czarny tekst -->
<div class="hover:shadow-k-main/50">  <!-- Cień w kolorze k-main z 50% opacity -->
```

---

## Przydatne wskazówki

### 1. **Spacing scale:**

- Wszystkie wartości liczbowe w Tailwind to wielokrotności **0.25rem (4px)**
- `p-4` = `1rem` = `16px`
- `m-8` = `2rem` = `32px`

### 2. **Responsive design:**

- Tailwind używa **mobile-first** - style bez prefiksu są dla mobile
- Dodaj prefiksy dla większych ekranów: `md:`, `lg:`, `xl:`

### 3. **Hover states:**

- Zawsze dodawaj `transition` dla smooth efektów
- `duration-300` (300ms) to dobry standard

### 4. **Focus states:**

- Używaj `focus:outline-none` + `focus:ring-2` dla custom focusa
- Zawsze dodawaj focus states dla accessibility

### 5. **Kolory z opacity:**

- Używaj `/` do określenia opacity: `bg-red-500/20` = czerwone tło z 20% opacity
- Przydatne dla subtle backgrounds i overlay

---

**Ten przewodnik zawiera wszystkie klasy Tailwind CSS używane w projekcie IT Help Desk System!** 🎨
