CREATE DATABASE VYSA
GO

use VYSA
go

create table Pessoas
(
    IdPessoa	int             not null      primary key     identity,
    nome        varchar(50)     not null,
    cpf         varchar(14)     not null      unique,
	endereco	varchar(50)		not null,
    status      int             not null,
    telefone    varchar(15)	    not null      unique,
    endEmail    varchar(50)     not null      unique,
    CHECK       (status in (0,1)) -- 1 ativo, 0 inativo
)
go


create table Clientes
(
    pessoa_id	    int                 not null,
    idade		    int                 not null,
    primary key (pessoa_id), 
    foreign key (pessoa_id)     references Pessoas(IdPessoa) 
)
go

create table Funcionarios
(
    pessoa_id   int             not null,
    salario     decimal(10,2)   not null,
	login		varchar(30)		not null,
	senha		varchar(30),
    check       (salario >= 1000),
	primary key (pessoa_id), 
    foreign key (pessoa_id)     references Pessoas(IdPessoa) 
)
go

create table Fornecedores
(
    pessoa_id		int             not null,
	empresa     	varchar(50)		not null,
	primary key (pessoa_id), 
    foreign key (pessoa_id)         references Pessoas(IdPessoa)
)
go

create table Pedidos
(
    IdPedido		int             not null    primary key identity,
    data			datetime        not null, 
    valor			decimal(10,2)   not null,
    tipo            int             not null,
    cliente_id		int                 null    references Clientes(pessoa_id),
    funcionario_id	int             not null    references Funcionarios(pessoa_id),
    CHECK			(valor > 0),
    CHECK           (tipo in (0, 1)) -- 1 delivery, 0 balcão
)
go

create table Categorias
(
    IdCategoria			int					not null		primary key		identity,
    nome		        varchar(40)			not null
)
go

create table Produtos
(
    IdProduto	int             not null    primary key     identity,
	nome		varchar(50)		not null,
    descricao   varchar(100)        null,
    estoque     int                 null    default 0,
    preco       decimal(10,2)       null    default 0,
	cod_barras	int,
	categoria_id	int				    null,
    fornecedor_id    int             null,
    status      int					null,
    CHECK       (estoque >= 0),
    CHECK       (preco >= 0),
    CHECK       (status >= 0 and status <= 1), -- 1 ativo, 0 inativo
	foreign key (categoria_id)    references Categorias(IdCategoria),
	foreign key (fornecedor_id)   references Fornecedores(pessoa_id)
)
go

create table Produtos_Pedidos
(
    pedido_id   int				not null    references Pedidos(IdPedido),
    produto_id  int				not null    references Produtos(IdProduto),
    qtd         int				not null,
    valor       decimal(10,2)   not null,
    CHECK       (qtd > 0),
    CHECK       (valor >0),
    primary key (pedido_id, produto_id)
)
go



CREATE VIEW vProdutos
AS
SELECT Produtos.*, Categorias.Nome as Categoria
FROM Produtos
JOIN Categorias ON Produtos.categoria_id = Categorias.IdCategoria
GO

CREATE VIEW vEstoqueBaixo
AS
SELECT Produtos.*
FROM Produtos
WHERE Produtos.estoque <= 20
GO


CREATE PROCEDURE sp_add_funcionario
    (
        @nome varchar(50), @cpf varchar(14), 
        @endereco varchar(50), @status int, @salario decimal(10,2), 
        @login varchar(30), @senha varchar(30),
        @endEmail varchar(100), @telefone varchar(15)
    )
AS
	INSERT INTO Pessoas VALUES (@nome, @cpf, @endereco, @status, @telefone, @endEmail)
	INSERT INTO Funcionarios VALUES (@@IDENTITY, @salario, @login, @senha)
GO


CREATE PROCEDURE sp_update_funcionario 
    (
        @nome varchar(50), @cpf varchar(14), 
        @endereco varchar(50), @status int, @salario decimal(10,2), 
        @login varchar(30), @senha varchar(30),
        @endEmail varchar(100), @telefone varchar(15), @pessoa_id int
    )
AS
	UPDATE Pessoas set nome = @nome, cpf = @cpf, endereco = @endereco, status = @status, endEmail = @endEmail, telefone = @telefone where IdPessoa = @pessoa_id
	UPDATE Funcionarios set salario = @salario, login = @login, senha = @senha where pessoa_id = @pessoa_id
GO


CREATE VIEW vFuncionarios
AS
SELECT Pessoas.*, Funcionarios.*
FROM Funcionarios
JOIN Pessoas ON Funcionarios.pessoa_id = Pessoas.IdPessoa
GO


CREATE PROCEDURE sp_add_cliente 
        (
            @nome varchar(50), @cpf varchar(14), 
            @endereco varchar(50), @status int, 
            @endEmail varchar(100), @telefone varchar(15), @idade int
        )
AS
	INSERT INTO Pessoas VALUES (@nome, @cpf, @endereco, @status, @telefone, @endEmail)
	INSERT INTO Clientes VALUES (@@IDENTITY, @idade)
GO

CREATE PROCEDURE sp_update_cliente 
    (
            @nome varchar(50), @cpf varchar(14), 
            @endereco varchar(50), @status int, 
            @endEmail varchar(100), @telefone varchar(15), 
            @idade int, @pessoa_id int
    )
AS
	UPDATE Pessoas set nome = @nome, cpf = @cpf, endereco = @endereco, status = @status, endEmail = @endEmail, telefone = @telefone where IdPessoa = @pessoa_id
	UPDATE Clientes set idade = @idade where pessoa_id = @pessoa_id
GO

CREATE VIEW vClientes
AS
SELECT Pessoas.*, Clientes.*
FROM Clientes
JOIN Pessoas ON Clientes.pessoa_id = Pessoas.IdPessoa
GO


CREATE PROCEDURE sp_add_fornecedor 
        (
            @nome varchar(50), @cpf varchar(14), 
            @endereco varchar(50), @status int, @empresa varchar(50),
            @endEmail varchar(100), @telefone varchar(15)
        )
AS
	INSERT INTO Pessoas VALUES (@nome, @cpf, @endereco, @status, @telefone, @endEmail)
	INSERT INTO Fornecedores VALUES (@@IDENTITY, @empresa)
GO

CREATE PROCEDURE sp_update_fornecedor 
    (
        @nome varchar(50), @cpf varchar(14), 
        @endereco varchar(50), @status int, 
        @empresa varchar(50), @endEmail varchar(100), 
        @telefone varchar(15), @pessoa_id int
    )
AS
	UPDATE Pessoas set nome = @nome, cpf = @cpf, endereco = @endereco, status = @status, endEmail = @endEmail, telefone = @telefone where IdPessoa = @pessoa_id
	UPDATE Fornecedores set empresa = @empresa where pessoa_id = @pessoa_id
GO

CREATE VIEW vFornecedores
AS
SELECT Pessoas.*, Fornecedores.*
FROM Fornecedores
JOIN Pessoas ON Fornecedores.pessoa_id = Pessoas.IdPessoa
GO