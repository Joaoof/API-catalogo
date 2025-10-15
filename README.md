
# 🚀 API Catálogo: Sua Base para E-commerce em .NET!

Bem-vindo(a) ao repositório da **API Catálogo**! Esta é a solução **RESTful** completa para gerenciar seu catálogo de produtos e categorias, construída com o poderoso ecossistema **.NET** e seguindo as **melhores práticas** de desenvolvimento de APIs modernas.

Aqui, focamos em código limpo, performance e facilidade de manutenção!

## ✨ O que Move o Projeto? (Tecnologias)

Construímos esta API com ferramentas de ponta para garantir estabilidade e escalabilidade:

- **.NET / C# (6.0+):** O coração da nossa aplicação, garantindo velocidade e robustez.
- **Entity Framework Core:** Nosso ORM de confiança para um acesso a dados eficiente e elegante.
- **AutoMapper:** Dizendo adeus ao código *boilerplate*! Mapeamento objeto-objeto de forma automática e inteligente.
- **Newtonsoft.Json:** O mestre da serialização, cuidando dos seus dados JSON.
- **Docker:** Para que você possa rodar o projeto em qualquer lugar com um único comando!
- **Arquitetura em Camadas:** Organização e separação de responsabilidades para um código de fácil entendimento.

## 🌟 Recursos que Você Vai Amar

Esta API está recheada de recursos essenciais para qualquer projeto moderno:

- ✅ **CRUD Completo:** Gerencie seus produtos e categorias sem complicação.
- ✅ **Paginação Inteligente:** Listas grandes? Sem problemas! Resultados paginados e otimizados.
- ✅ **Atualizações Flexíveis (PATCH):** Permite a atualização parcial de recursos. Mude apenas o que for necessário!
- ✅ **DTOs (Data Transfer Objects):** Controle total sobre os dados que entram e saem.
- ✅ **Mapeamento Automático:** Graças ao AutoMapper, menos linhas de código, mais tempo para inovar.
- ✅ **Pronta para Containers:** Implantação descomplicada com **Docker**.

---

## 🛠️ Como Colocar Para Rodar (Setup)

Pronto para começar? Siga estes passos simples:

### Pré-requisitos

Você precisará ter instalado na sua máquina:

1.  [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
2.  **SQL Server** (ou outro banco de dados que suporte o Entity Framework Core).
3.  [Docker](https://www.docker.com/) (Se quiser rodar em um container, super recomendado!)

### 1. Clonando o Projeto

Abra seu terminal e execute:

```bash
git clone [https://github.com/Joaoof/API-catalogo.git](https://github.com/Joaoof/API-catalogo.git)
cd API-catalogo
````

### 2\. Configurando o Banco de Dados

Primeiro, edite o arquivo `appsettings.json` na pasta `ApiCatalogo` com as informações da sua conexão:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CatalogoDB;User Id=seu_usuario;Password=sua_senha;"
  }
}
```

Em seguida, aplique as migrations para criar o banco de dados e as tabelas:

```bash
dotnet ef database update
```

### 3\. Execução

#### Opção A: Pelo .NET CLI (Direto)

Ideal para desenvolvimento e testes rápidos:

```bash
cd ApiCatalogo
dotnet restore
dotnet run
```

#### Opção B: Pelo Docker (O jeito mais limpo)

Construa a imagem e inicie o container:

```bash
docker build -t api-catalogo .
docker run -p 5000:80 api-catalogo
```

🎉 Sua API estará no ar, pronta para receber requisições em `http://localhost:5000` (ou `https://localhost:5001`)\!

-----

## 🧭 Mapeamento dos Endpoints

Aqui está um guia rápido dos principais caminhos que você pode explorar:

### Produtos 🛍️

| Método | Caminho | Descrição |
| :--- | :--- | :--- |
| `GET` | `/api/produtos` | Lista todos os produtos (com paginação) |
| `GET` | `/api/produtos/{id}` | Busca um produto específico pelo ID |
| `POST` | `/api/produtos` | Cria um **novo** produto |
| `PUT` | `/api/produtos/{id}` | Atualiza o produto **completo** |
| `PATCH` | `/api/produtos/{id}` | Atualiza o produto **parcialmente** |
| `DELETE` | `/api/produtos/{id}` | Remove um produto |

### Categorias 🏷️

| Método | Caminho | Descrição |
| :--- | :--- | :--- |
| `GET` | `/api/categorias` | Lista todas as categorias (com paginação) |
| `GET` | `/api/categorias/{id}` | Busca uma categoria pelo ID |
| `POST` | `/api/categorias` | Cria uma **nova** categoria |
| `PUT` | `/api/categorias/{id}` | Atualiza a categoria |
| `DELETE` | `/api/categorias/{id}` | Remove uma categoria |

### Detalhe da Paginação

Quer controlar o fluxo de dados? Use estes *query parameters*:

```http
GET /api/produtos?pageNumber=1&pageSize=10
```

  - `pageNumber`: Qual página você quer ver? (Padrão: 1)
  - `pageSize`: Quantos itens por página? (Padrão: 10, Máx: 50)

-----

## 🏗️ Mergulhando na Estrutura

A organização é chave\! A arquitetura do projeto foi pensada para ser limpa e escalável:

```
ApiCatalogo/
├── Controllers/         # Onde a mágica do roteamento HTTP acontece.
├── Models/              # Suas entidades de domínio (Produto, Categoria).
├── DTOs/                # Os objetos que viajam pela rede.
├── Context/             # A ponte com o Entity Framework (DbContext).
├── Repository/          # Abstração do acesso a dados. O padrão Repository!
├── Mappings/            # Regras de mapeamento do AutoMapper.
├── Extensions/          # Códigos úteis que estendem funcionalidades do .NET.
└── Dockerfile           # A receita para o seu container.
```

## 🧠 Boas Práticas em Destaque

Valorizamos a qualidade do código. Veja o que aplicamos aqui:

  - **AutoMapper:** Simplificando a vida\! Evita a repetição de código ao transferir dados entre `Models` e `DTOs`.
  - **Repository Pattern:** Garantimos a separação de preocupações (Separation of Concerns). Se precisarmos mudar o banco de dados, a camada de acesso é a única a ser tocada\!
  - **HTTP PATCH:** Implementação robusta para garantir que suas atualizações sejam eficientes e atômicas.

-----

## 🤝 Quer Contribuir?

Adoramos colaboração\! Se você encontrou um bug, tem uma ideia de *feature* ou quer otimizar algo, sinta-se em casa para contribuir:

1.  Dê um `Fork` no projeto.
2.  Crie uma nova *branch* para a sua contribuição: (`git checkout -b feature/minha-melhoria`).
3.  Faça seus *commits* com mensagens claras (`git commit -m 'feat: Adiciona super melhoria X'`).
4.  Envie suas mudanças (`git push origin feature/minha-melhoria`).
5.  Abra um **Pull Request** e vamos analisar juntos\!

## 📝 Licença

Este projeto está liberado sob a licença **MIT**. Sinta-se à vontade para usar e modificar. Veja o arquivo [LICENSE](https://www.google.com/search?q=LICENSE) para detalhes completos.

## 👤 Autor

Se precisar de ajuda ou tiver dúvidas, pode falar diretamente com o criador:

**Joaoof**

  - GitHub: [@Joaoof](https://github.com/Joaoof)

-----
