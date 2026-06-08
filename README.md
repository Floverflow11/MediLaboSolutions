# MediLabo Solutions

Application de gestion de dossiers patients et d'évaluation du risque diabétique, construite en architecture microservices.

## Lancer le projet

### Prérequis
- Docker Desktop

### Démarrage
```
docker compose up --build
```

Ouvrir `http://localhost:5295` dans le navigateur.

## Green Code

### Qu'est-ce que le Green Code ?

Le Green Code vise à réduire l'empreinte environnementale du logiciel en optimisant la consommation de ressources (CPU, mémoire, réseau, stockage). Un code inefficace consomme davantage d'énergie côté serveur et côté client, ce qui contribue aux émissions de CO2 du secteur numérique.

### Comment identifier les parties d'un code qui consomment inutilement ?

- **Profiling** : mesurer la consommation CPU/mémoire à l'exécution pour identifier les hotspots
- **Requêtes inutiles** : détecter les appels réseau ou base de données redondants
- **Allocations mémoire excessives** : objets créés inutilement, collections non réutilisées
- **Dépendances lourdes** : librairies importées pour un usage minimal

### Recommandations d'amélioration pour MediLabo

#### 1. Mettre en cache les résultats
L'Assessment API appelle Patient API et PatientHistory API à chaque requête.
Si le même patient est consulté fréquemment, un cache éviterait des appels réseau et des requêtes base de données
inutiles.

#### 2. Optimiser les requêtes en base de données
Dans Patient API, l'endpoint `GET /patients` retourne la totalité des patients sans pagination. Sur un large volume de données, cela charge inutilement la mémoire et le réseau. Ajouter une pagination réduirait significativement la consommation.

#### 3. Limiter le over-fetching
Les modèles `Patient` retournent tous les champs y compris l'adresse et le téléphone, même quand seule une partie des données sont nécessaires (ex: assessment). Utiliser des DTOs adaptés à chaque contexte réduirait la quantité de données transportées.

#### 4. Images Docker plus légères
Les Dockerfiles utilisent `aspnet:10.0` comme image de base. Passer sur des images `alpine` réduirait la taille des images et donc la consommation de stockage et de bande passante lors des déploiements.

#### 5. Optimiser le seed data
Dans PatientHistory API, le seed data vérifie `if (!context.Notes.Any())` à chaque
redémarrage de conteneur, ce qui effectue systématiquement une requête base de données
même en production. Privilégier `IsDevelopment()` pour limiter le seed au seul
environnement de développement.