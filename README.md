# Shop For Video Games (Magazin De Jocuri Video)
## An online shop for video games (games for PC only), has administrator mode and client mode.
This project is created in C# and it is helped by a database. This is also my project for finishig 12th grade in highschool for my baccalaureate degree, that's why my whole project is in Romanian instead of English.

##Here it is a short introduction about the project:

**1. Description of the topic and motivation of the choice**
The topic of the paper is the manager of a video game store. I chose this theme because a friend's passion for video games led me to create a small game store for those who are passionate, like him, so that it is much easier for them to choose a game.
This program is mostly aimed at gamers who want to buy more games, but don't know the details about the price, gameplay (ie the description of the game and what it's role is), what platforms it can be played on, or whether it can be played in teams or alone .

**2. Why I chose the language to solve the problem**
The project was made in the C# language, in the Microsoft Visual Studio program. The C# language is one of the most used programming languages, being simple, modern and very flexible in terms of developing applications and their portability. I chose this programming language because I studied it in 12th grade computer science classes, and during this year I acquired the necessary knowledge to create such a program.

**3. Description of database and tables**
The program database is made in the Microsoft Office Access program and is an important part of the project. It consists of 7 tables, "Users", "Games", "Developers", "Genre", "Receipts", "Year", "Photos". Which contain fields with the information necessary for the running and operation of the program.

Based on these tables, the program performs the following operations:
- logging into the account based on a username and password
- view games
- purchase games
- add/modify/delete games
- sending orders

**4. Detailed description of the application**
The program is accessed based on a buyer or administrator account. When opening the program, a window appears for authentication. 

If a buyer has connected, after authentication, the program opens with the following two options: View games/ Buy Game. In his account, the buyer has access to the entire list of games on the store, can view all the details, and after ordering as many games as he wants, if the game he wants is in stock. It also has the option to search for games by certain criteria: Developer, Genre, Mode or Platform. In the "Buy Game" option, he can see the games that are in stock and their price, if he changes his mind, he can remove a certain game or several from the basket.

The administrator account, as opposed to the buyer account, additionally allows adding, modifying and deleting games, as well as viewing the processing orders made by users. In "Modify products" the administrator has access to all games. He can change the dates, the number of copies, the cover of the game and the initial price, and the deletion is done by selecting the game from the list and pressing the "Delete game" button. The operation of adding a product consists of entering all its data and choosing a cover. Also, after entering the new game, the admin can see on the right side the complete list, plus the added game. At the same time, in "View orders" you can see all the orders placed or completed with all the appropriate details: buyer username, product ID, order placement date, and for already completed orders, the shipping date and the amount collected. On the "Ship the order" button, he can send orders to pending users.
