# Sistema de Emissão de NF-e

Sistema de terminal em C# para emitir notas fiscais, gerenciar clientes e imprimir notas em diferentes formatos. Feito aplicando 4 padrões de projeto: **Factory Method**, **Observer**, **Singleton** e **Strategy**.

---

## Como rodar

```bash
cd SistemaDeNFe
dotnet run
```

---

## Estrutura de pastas

```
SistemaDeNFe/
├── Program.cs                          → Ponto de entrada, menu principal
├── Models/                             → Dados simples (o que o sistema guarda)
│   ├── Cliente.cs
│   └── Produto.cs
├── Patterns/                           → Os 4 padrões de projeto
│   ├── Singleton/
│   │   └── EmpresaEmissora.cs
│   ├── Factory/
│   │   ├── NotaFiscal.cs
│   │   ├── NotaFiscalProduto.cs
│   │   ├── NotaFiscalServico.cs
│   │   └── NotaFiscalFactory.cs
│   ├── Observer/
│   │   ├── IObservadorNfe.cs
│   │   ├── NotificadorEmail.cs
│   │   └── EmissorDeNotas.cs
│   └── Strategy/
│       ├── IEstrategiaImpressao.cs
│       ├── ImpressaoTxt.cs
│       ├── ImpressaoJson.cs
│       └── ContextoImpressao.cs
├── Repositories/                       → Onde os dados ficam guardados em memória
│   ├── EstoqueRepository.cs
│   ├── ClienteRepository.cs
│   └── NfeRepository.cs
└── Menus/                              → O que o usuário vê na tela
    ├── MenuNfe.cs
    └── MenuClientes.cs
```

---

## O que cada classe faz

### `Program.cs`
É a primeira coisa que roda quando você executa o programa. Ele cria os repositórios e os menus, e fica em loop mostrando o menu principal até o usuário digitar 0 para sair.

---

### Pasta `Models` — os dados

#### `Cliente.cs`
Guarda as informações de um cliente: **nome**, **CPF** e **email**. Nada mais do que isso.

#### `Produto.cs`
Guarda as informações de um produto: **id**, **nome** e **preço**.

---

### Pasta `Repositories` — onde os dados vivem

#### `EstoqueRepository.cs`
Uma lista fixa com os 4 produtos da loja (Produto 1 a 4, de R$20 a R$50). Tem um método que busca produto pelo ID. Os produtos não mudam durante a execução — é o "catálogo" da loja.

#### `ClienteRepository.cs`
Guarda a lista de clientes que foram cadastrados. Permite adicionar, listar, remover e buscar cliente por CPF. Os dados ficam na memória.

#### `NfeRepository.cs`
Guarda todas as notas fiscais que foram emitidas. Permite salvar, listar todas e buscar uma nota pelo número.

---

### Pasta `Patterns` — os padrões de projeto

---

#### 🏭 Factory Method — `Patterns/Factory/`

> **Ideia central:** quem pede a nota não precisa saber como ela é criada.

##### `NotaFiscal.cs`
É o "molde" de toda nota fiscal. Toda nota tem número (gerado automaticamente), data de emissão, dados do emissor e dados do cliente. Ela define que toda nota **precisa** ter um método `ExibirCorpo()`, mas não diz como — cada tipo de nota implementa do seu jeito.

##### `NotaFiscalProduto.cs`
É uma nota fiscal que carrega uma **lista de produtos**. Cada vez que você adiciona um produto, ela soma o preço no total. O `ExibirCorpo()` dela mostra a lista de produtos e o subtotal.

##### `NotaFiscalServico.cs`
É uma nota fiscal que carrega apenas um **valor de serviço** e uma descrição. O `ExibirCorpo()` dela mostra a descrição e o valor cobrado.

##### `NotaFiscalFactory.cs`
Contém três coisas:
- **`NotaFiscalFactory`** — define que toda fábrica precisa ter o método `CriarNota()`, mas não implementa.
- **`FabricaNotaProduto`** — fábrica que cria uma `NotaFiscalProduto` quando chamada.
- **`FabricaNotaServico`** — fábrica que cria uma `NotaFiscalServico` quando chamada.

O menu nunca escreve `new NotaFiscalProduto()` diretamente. Ele chama `fabrica.CriarNota()` e recebe a nota pronta, sem precisar saber o tipo.

---

#### 👀 Observer — `Patterns/Observer/`

> **Ideia central:** quando uma nota é emitida, quem precisar ser avisado é avisado automaticamente — sem que o menu precise chamar ninguém diretamente.

##### `IObservadorNfe.cs`
É um contrato (interface). Diz que qualquer classe que queira ser avisada sobre a emissão de uma nota precisa ter o método `AoEmitirNota(nota)`.

##### `NotificadorEmail.cs`
Implementa esse contrato. Quando `AoEmitirNota()` é chamado, ele exibe na tela a mensagem simulando o envio de e-mail para o cliente.

##### `EmissorDeNotas.cs`
É quem controla a lista de "ouvintes". Você registra nele quem quer ser avisado (ex: `NotificadorEmail`). Quando uma nota é emitida, o menu chama `Notificar(nota)` e ele avisa todo mundo da lista automaticamente.

**Fluxo:** nota salva → `EmissorDeNotas.Notificar(nota)` → chama `AoEmitirNota()` em cada ouvinte → `NotificadorEmail` exibe o e-mail.

---

#### 🔒 Singleton — `Patterns/Singleton/`

> **Ideia central:** os dados da empresa existem uma única vez em toda a execução.

##### `EmpresaEmissora.cs`
Guarda o CNPJ, razão social, endereço e e-mail da empresa emissora. Tem três características especiais:

1. O construtor é **privado** — ninguém de fora consegue fazer `new EmpresaEmissora()`.
2. Existe um campo estático `_instancia` que guarda o único objeto criado.
3. A propriedade `Instancia` cria esse objeto na primeira vez que é chamada e, nas próximas vezes, devolve o mesmo que já estava criado.

Toda nota fiscal acessa os dados da empresa via `EmpresaEmissora.Instancia` — nunca criando um objeto novo.

---

#### 🎯 Strategy — `Patterns/Strategy/`

> **Ideia central:** o formato de impressão pode ser trocado sem mexer no resto do código.

##### `IEstrategiaImpressao.cs`
Contrato que todo formato de impressão precisa seguir: ter o método `Imprimir(nota)`.

##### `ImpressaoTxt.cs`
Implementa a impressão em formato texto simples — como seria um arquivo `.txt`. Exibe as informações da nota em linhas formatadas.

##### `ImpressaoJson.cs`
Implementa a impressão em formato JSON — como seria um arquivo `.json`. Monta manualmente o objeto com chaves e valores.

##### `ContextoImpressao.cs`
Recebe qual estratégia usar e delega a impressão para ela. Ele não sabe se vai imprimir TXT ou JSON — só chama `estrategia.Imprimir(nota)`. Quem decide o formato é o menu, antes de passar a estratégia para ele.

---

### Pasta `Menus` — o que o usuário vê

#### `MenuClientes.cs`
Exibe o submenu de clientes e chama os métodos do `ClienteRepository` conforme a opção escolhida: adicionar, listar ou remover.

#### `MenuNfe.cs`
É o menu mais importante — conecta todos os padrões:

- **Emitir NFe:** escolhe a fábrica (Factory Method), cria a nota, preenche os dados, salva no repositório e chama o `EmissorDeNotas.Notificar()` (Observer).
- **Listar NFes:** lê todas as notas salvas no `NfeRepository` e exibe um resumo.
- **Imprimir NFe:** pergunta o número da nota e o formato, cria a estratégia correta (Strategy) e chama `ContextoImpressao.Executar()`.

---

## Como as classes se conectam — o fluxo completo

```
Usuário digita "Emitir NFe"
        │
        ▼
MenuNfe escolhe a fábrica
   FabricaNotaProduto  ou  FabricaNotaServico     ← Factory Method
        │
        ▼
fabrica.CriarNota() devolve a nota
   NotaFiscalProduto  ou  NotaFiscalServico
        │
        ▼
Nota busca dados do emissor
   EmpresaEmissora.Instancia                      ← Singleton
        │
        ▼
Usuário preenche os dados (produtos ou serviço)
        │
        ▼
Nota é salva no NfeRepository
        │
        ▼
EmissorDeNotas.Notificar(nota)                    ← Observer
        │
        ▼
NotificadorEmail.AoEmitirNota(nota)
   "E-mail enviado para cliente@email.com"
```

```
Usuário digita "Imprimir NFe"
        │
        ▼
MenuNfe pergunta o número da nota e o formato
        │
        ▼
Cria a estratégia escolhida                       ← Strategy
   ImpressaoTxt  ou  ImpressaoJson
        │
        ▼
ContextoImpressao.Executar(nota)
        │
        ▼
estrategia.Imprimir(nota)
   Exibe a nota no formato escolhido
```

---

## Os 4 padrões em uma linha cada

| Padrão | O problema que ele resolve |
|---|---|
| **Factory Method** | O menu não precisa saber como criar cada tipo de nota — só pede para a fábrica criar. |
| **Observer** | O menu não precisa chamar o e-mail diretamente — quem quiser ser avisado se registra e é chamado automaticamente. |
| **Singleton** | Os dados da empresa existem uma única vez — não dá para criar dois objetos `EmpresaEmissora` por acidente. |
| **Strategy** | O formato de impressão pode ser trocado em tempo de execução — TXT hoje, JSON amanhã, sem mexer no menu. |
