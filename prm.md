# Générateur de Prompt de Détection de Discours Fallacieux

## Objectif
Générer un prompt prêt à l'emploi pour analyser un discours et identifier les sophismes (fallacies), avec une sortie structurée exploitable dans une application **.NET 10 / Blazor WebAssembly** (templating **Fluid**, UI **BlazorBluePrint**) et publication **GitHub Pages**.

## Variables d'entrée
- `langue` : langue de réponse (ex. `fr`)
- `contexte` : contexte du discours (ex. débat politique, publicité, réseau social)
- `discours` : texte à analyser
- `niveau_detail` : `court` | `normal` | `détaillé`

## Template de prompt (Fluid)

```liquid
Tu es un analyste expert en raisonnement critique.

Tâche : détecter les discours fallacieux dans le texte ci-dessous.

Contraintes :
1. Répondre en {{ langue | default: "fr" }}.
2. Identifier uniquement les fallacies réellement présentes dans le texte.
3. Citer des extraits exacts du texte pour chaque détection.
4. Expliquer brièvement pourquoi chaque extrait est fallacieux.
5. Proposer une reformulation non fallacieuse.
6. Si aucune fallacy n'est détectée, retourner une liste vide.

Contexte : {{ contexte | default: "non précisé" }}
Niveau de détail : {{ niveau_detail | default: "normal" }}

Texte à analyser :
"""
{{ discours }}
"""

Retourne UNIQUEMENT un JSON valide au format :
{
  "langue": "string",
  "resume": "string",
  "fallacies": [
    {
      "type": "string",
      "gravite": "faible|moyenne|élevée",
      "extrait": "string",
      "explication": "string",
      "reformulation": "string"
    }
  ],
  "score_fallacieux": 0
}
```

## Types de fallacies recommandés
- Attaque ad hominem
- Homme de paille
- Faux dilemme
- Généralisation hâtive
- Appel à l'autorité
- Appel à l'émotion
- Pente glissante
- Fausse causalité (post hoc)
- Cherry picking
- Argument circulaire

## Exemple minimal de rendu

### Entrée
- `langue`: `fr`
- `contexte`: `débat télévisé`
- `niveau_detail`: `normal`
- `discours`: `Mon opposant a tort sur l'économie, il est incompétent depuis toujours.`

### Sortie attendue (forme)
```json
{
  "langue": "fr",
  "resume": "Le texte contient au moins une attaque ad hominem.",
  "fallacies": [
    {
      "type": "Attaque ad hominem",
      "gravite": "moyenne",
      "extrait": "il est incompétent depuis toujours",
      "explication": "L'argument vise la personne plutôt que le fond de sa position économique.",
      "reformulation": "Je ne suis pas d'accord avec sa proposition économique car ses hypothèses budgétaires ne sont pas justifiées."
    }
  ],
  "score_fallacieux": 62
}
```
