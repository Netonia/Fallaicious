# PRD - Fallaicious : Générateur de Prompt de Détection de Discours Fallacieux

## Objectif

Application Blazor WebAssembly (.NET 10) permettant de générer un prompt
structuré destiné à une IA générative afin d'analyser un discours et
détecter des raisonnements fallacieux.

------------------------------------------------------------------------

## Stack Technique

-   .NET 10
-   Blazor WebAssembly
-   slnx
-   Fluid (Liquid parser)
-   BlazorBlueprint UI
-   LocalStorage navigateur
-   Déploiement GitHub Pages via GitHub Actions

------------------------------------------------------------------------

## Fonctionnalités

### 1. Choix du type d'entrée

Deux modes :

1)  Retranscription d'un discours individuel\
2)  Conversation entre deux personnes (A / B)

------------------------------------------------------------------------

### 2. Contexte (optionnel)

-   Résumé du sujet
-   Lieu
-   Date
-   Informations sur A
-   Informations sur B
-   Relation entre A et B
-   Autres éléments importants

Les champs non remplis ne doivent pas apparaître dans le prompt final.

------------------------------------------------------------------------

### 3. Template Liquid éditable

Le template doit :

-   Définir ce qu'est un discours fallacieux
-   Lister les procédés rhétoriques (appel à la peur, faux dilemme,
    etc.)
-   Lister les biais cognitifs existants
-   Détecter :
    -   Attaques personnelles
    -   Détournement du sujet
    -   Négation de faits établis
    -   Volonté de domination argumentative
    -   Présentation pseudo-vertueuse

Le template doit être modifiable avant génération.

------------------------------------------------------------------------

## Modèle de données

-   AnalysisRequest
-   InputModel
-   ContextModel

------------------------------------------------------------------------

## UI

Layout avec :

-   Sidebar gauche (historique LocalStorage)
-   Zone centrale (formulaire + template + génération)
-   Toggle thème Light / Dark

------------------------------------------------------------------------

## Historique

Stockage LocalStorage (navigateur) d'analyses :

-   Sauvegarde
-   Chargement
-   Suppression

------------------------------------------------------------------------

# 🚀 Déploiement GitHub Pages

## Objectif

Déploiement automatique de l'application Blazor WASM sur GitHub Pages
via GitHub Actions.

------------------------------------------------------------------------

## Configuration Repository

-   Branch principale : main
-   Publication : branche gh-pages
-   Permissions GitHub Pages activées

------------------------------------------------------------------------

## Workflow GitHub Action

Créer le fichier :

.github/workflows/deploy.yml

### Contenu du Workflow

``` yaml
name: Deploy Blazor WASM to GitHub Pages

on:
  push:
    branches:
      - main

permissions:
  contents: write
  pages: write
  id-token: write

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.0.x'

      - name: Publish Blazor App
        run: dotnet publish -c Release -o release

      - name: Rewrite base href
        run: |
          sed -i 's|<base href="/" />|<base href="/REPO_NAME/" />|g' release/wwwroot/index.html

      - name: Deploy to GitHub Pages
        uses: peaceiris/actions-gh-pages@v3
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_dir: release/wwwroot
```

------------------------------------------------------------------------

## Points Importants

-   Adapter REPO_NAME au nom du repository
-   Vérifier `<base href>`{=html} pour GitHub Pages
-   Activer GitHub Pages dans Settings → Pages

------------------------------------------------------------------------

## Résultat attendu

À chaque push sur main :

-   Build automatique
-   Publication sur GitHub Pages

------------------------------------------------------------------------
