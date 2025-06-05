# DevelopmentChallenge
Desafio Técnico – Refatoração de Código em C#


# Cenário

Nosso sistema possui um método chamado Print (List<GeometricShape> shapes,
int language), localizado na classe GeometricShape.cs. Ele gera um relatório com
base em uma lista de formas geométricas e o idioma fornecido.
Problema:

Esse código está difícil de manter. É complicado adicionar novas formas ou novos
idiomas. Queremos que ele seja mais flexível para evoluções futuras.


# Objetivo

Refatore o código para que:

• Novas formas geométricas possam ser adicionadas facilmente;

• Novos idiomas possam ser suportados com mínimo esforço;

• Os testes existentes (NUnit) continuem passando corretamente.


# Regras


• Você pode alterar tudo (estrutura, métodos, testes);

• O código final deve passar todos os testes;

• Há um TODO no código com requisitos adicionais de negócio e técnicos;

• Se possível, atualize o projeto para Visual Studio 2022, .NET 6+ e use xUnit
(isso dará pontos extras).


# Tecnologias

• Linguagem: C#

• Framework atual: .NET Framework 4.6.2

• IDE: Visual Studio 2019 (atualização para VS2022 é opcional)

• Testes: NUnit (opcional migrar para xUnit)


# Tempo estimado

Até 2 horas.

Entrega:

• Suba sua solução para um repositório público no GitHub ou GitLab;

• Mantenha o README atualizado, explicando as decisões que você tomou;

• Avise quando concluir.


# SOLUÇÃO

• Atualizado para .NET 8
![image](https://github.com/user-attachments/assets/8bbf28b7-6f26-449e-b69e-471a11e1f6b0)

• Adicionado Resources para diferentes idiomas incluindo (EN, ES, PT, IT)
![image](https://github.com/user-attachments/assets/f8ea9cef-feec-44ae-8999-c8ca9d504912)

• Adicionado Arquivo JSON (shapes.json) para Formas Geométricas e suas Fórmulas facilitando a inclusão de novas formas geométricas.
![image](https://github.com/user-attachments/assets/04ba081c-0042-4001-b2f0-e4464c5a2415)

• Adicionado Biblioteca NCalc-Edge para efetuar calculo a partir de expressão contida no arquivo JSON (shapes.json).
![image](https://github.com/user-attachments/assets/59840d1d-0a64-4960-904d-fce02c841b35)

• Adicionado Formas Geométricas (todos os triângulos, retângulo e trapézio)

• Teste em xUnit validando cálculos e tradução
![image](https://github.com/user-attachments/assets/6bbc3144-aff1-41ec-ae7e-d163efe332a4)
