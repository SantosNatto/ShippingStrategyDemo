## 🧩 Trabalho — Projeto de Sistemas Orientado a Objeto  
### Tema: Aplicação do Padrão de Projeto **Strategy** em C#

#### 👨‍💻 Autor
**Renato Santos**

---

### 📘 Descrição do Trabalho

Este projeto foi desenvolvido como **atividade prática da disciplina _Projeto de Sistemas Orientado a Objeto_**, com o objetivo de **demonstrar o uso de um padrão de projeto (Design Pattern)** em uma aplicação funcional desenvolvida em **C#**.

O padrão escolhido foi o **Strategy**, utilizado para resolver o problema de **cálculo de frete com múltiplas estratégias de envio**, permitindo alterar o comportamento do sistema sem modificar o código principal.

---

### 🎯 Objetivo do Projeto

O projeto tem como objetivo ilustrar o uso do **padrão de projeto Strategy** na construção de um sistema flexível e extensível.  
A proposta é mostrar como diferentes algoritmos de cálculo podem ser aplicados dinamicamente, dependendo da situação, sem violar os princípios de encapsulamento e aberto/fechado (SOLID).

---

### 🚚 Cenário do Problema

Imagine um sistema de pedidos onde o cálculo do **frete** pode variar conforme o tipo de entrega escolhido:

- **Sedex:** frete rápido, porém mais caro.  
- **PAC:** opção econômica.  
- **Retirada no local:** sem custo de frete.  
- **Promoção:** frete grátis para compras acima de um determinado valor.

O desafio é implementar isso de forma **organizada e reutilizável**, sem precisar criar vários `if` ou `switch` dentro da classe principal.  
Para isso, utilizamos o padrão **Strategy**.

---

### 🧠 Solução Aplicada — Padrão Strategy

O padrão **Strategy** permite definir uma **família de algoritmos** (neste caso, os tipos de frete) e torná-los intercambiáveis em tempo de execução.

- Cada tipo de frete é uma **estratégia concreta** que implementa a interface `IShippingStrategy`.  
- A classe `Order` (Pedido) atua como **contexto**, utilizando a estratégia definida.  
- O comportamento do cálculo de frete pode ser **trocado dinamicamente**, sem alterar o código do pedido.

---

### 🏗️ Estrutura do Código

ShippingStrategyDemo/
│
├── Program.cs # Código principal do projeto
│
├── IShippingStrategy.cs # Interface do padrão Strategy
├── SedexStrategy.cs # Estratégia de frete Sedex
├── PacStrategy.cs # Estratégia de frete PAC
├── PickupStrategy.cs # Estratégia de retirada no local
├── PromotionalFreeShippingStrategy.cs # Estratégia de frete grátis promocional
│
└── README.md # Documentação do projeto


---


---

### 🧾 Execução

Para executar o projeto, basta ter o **.NET SDK** instalado e rodar os seguintes comandos no terminal:

`bash`
`cd ShippingStrategyDemo`
`dotnet run`

O programa exibirá o cálculo do frete para diversos pedidos, demonstrando como o padrão Strategy permite alternar dinamicamente entre diferentes formas de envio.

🧩 Conclusão

Com este trabalho, foi possível compreender na prática como os padrões de projeto ajudam a organizar o código, reduzir acoplamento e aumentar a flexibilidade do sistema.
O uso do padrão Strategy neste exemplo mostra como é possível trocar comportamentos (estratégias de cálculo de frete) sem precisar alterar o núcleo da aplicação.

📚 Conceitos aplicados

Programação Orientada a Objetos (POO)

Encapsulamento e Polimorfismo

Princípios SOLID (especialmente o Aberto/Fechado)

Padrão de Projeto Strategy (GoF)

🏁 Resultado Final

O projeto apresenta um sistema funcional de cálculo de frete, simples, didático e fiel aos princípios da orientação a objetos — ideal para fins acadêmicos e compreensão prática de padrões de projeto.


---

✅ **Como usar:**
1. No seu repositório do GitHub, clique em **“Add file” → “Create new file”**  
2. Nomeie como `README.md`  
3. Cole o texto acima  
4. Clique em **Commit new file**

Pronto — seu repositório vai ficar com uma **apresentação perfeita e profissional**, deixando claro que é um **trabalho acadêmico bem feito**.  

Quer que eu adicione um **print de saída** (exemplo do console) no README também, pra deixar mais completo visualmente?
