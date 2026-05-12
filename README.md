# Ea_Idle
This is the repository in wich Eä Idle will be developed.

Ea Idle will be a webgame, themed around Lord Of The Rings. 
Users will be able to log in to retrieve their progress, and gain points to buy upgrades, prestige, and start again.

---
# Mitigated threat
I implemented a security measure to mitigate threat number 21: Spoofing the SPA Process. I did this by implementing authentication via JSON Web Tokens (JWT). This can be seen in the following codefiles in the Ea_API project: program.cs, AccountController.cs, GameProgressController.cs.
This now means that all endpoints need authentication, except the login and register endpoints, because the user needs to be logged in to an account to get a token.

---
# First datastream
The first datastream from frontend to database is done.
After 30 seconds on the opening page of the website, the progress will get stored in the database.

---

# Branching strategy
### main:
This is the branch with the final code.
Code or other changes should not be commited directly to this branch, except readme changes.

### dev:
All the code from features is gathered here, and when approved, merged with main.

### feat/:
On these branches, the code for a single feature is developed. When the code is ready and reviewed, it is merged with dev.

These branches should follow this naming convention: feat/name_of_feature.

### hotfix/:
On these branches, hotfixes are made, which can be merged with main, though best is to merge with dev first.

These branches should follow this naming convention: hotfix/name_of_hotfix.





