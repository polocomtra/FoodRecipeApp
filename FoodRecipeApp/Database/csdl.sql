drop database if exists dbFoodRecipeApp;
create database if not exists dbFoodRecipeApp;
use dbFoodRecipeApp;

create table Recipe(
    ID int not null auto_increment,
    Name varchar(255) character set utf8mb4,
    Description text character set utf8mb4,
    IsFavorite bool,
    
    primary key (ID)
);

create table Direction (
    ID int not null auto_increment,
    RecipeID int not null,
    ImageURL varchar(255),
    VideoURL varchar(255),
    Instruction text character set utf8mb4,
	Step int not null,
    
    primary key (ID, RecipeID)
);

create table Ingredient (
    ID int not null auto_increment,
    RecipeID int not null,
    Data varchar(255) character set utf8mb4,

    primary key (ID, RecipeID)
);

alter table Direction add foreign key (RecipeID) references Recipe(ID);
alter table Ingredient add foreign key (RecipeID) references Recipe(ID);