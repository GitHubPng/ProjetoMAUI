# Relatório de Alterações - ConsultorioApp

## Visão Geral
Implementei um sistema completo de gerenciamento de consultório médico em .NET MAUI com banco de dados SQLite local, permitindo operações CRUD (Create, Read, Update, Delete) para pacientes e agendamentos.

---

## 1. BANCO DE DADOS LOCAL (SQLite)

### 1.1 Pacotes NuGet Adicionados
- **sqlite-net-pcl**: Biblioteca ORM para SQLite em .NET
- **SQLitePCLRaw.bundle_green**: Dependências nativas do SQLite
- **SQLitePCLRaw.provider.dynamic_cdecl** e **SQLitePCLRaw.core**: Suporte multiplataforma

**Por quê?** O requisito exigia "banco de dados local". SQLite é ideal para aplicativos móveis: leve, rápido, não requer servidor.

### 1.2 Models Atualizados
**Arquivos:** `Models/Paciente.cs` e `Models/Consulta.cs`

**Alteração:** Adicionei atributos SQLite:
```csharp
[PrimaryKey, AutoIncrement]
public int Id { get; set; }
```

**Por quê?** Esses atributos informam ao SQLite que `Id` é chave primária e deve ser gerado automaticamente.

### 1.3 DatabaseContext Criado
**Arquivo:** `Data/DatabaseContext.cs`

**O que faz:**
- Gerencia a conexão com o banco de dados SQLite
- Cria as tabelas automaticamente na primeira execução
- Fornece métodos assíncronos para todas as operações CRUD

**Métodos implementados:**
- `GetPacientesAsync()` - Lista todos os pacientes
- `GetPacienteAsync(id)` - Busca um paciente específico
- `SavePacienteAsync(paciente)` - Insere ou atualiza paciente
- `DeletePacienteAsync(paciente)` - Exclui paciente
- Métodos equivalentes para Consultas, incluindo `GetConsultasByDateAsync(date)` para filtrar por data

**Por quê?** Centraliza toda a lógica de acesso a dados em um único lugar, facilitando manutenção.

---

## 2. CAMADA DE SERVIÇOS

### 2.1 PacienteService Reformulado
**Arquivo:** `Services/PacienteService.cs`

**ANTES:** Retornava dados fictícios (mock) de forma síncrona
**DEPOIS:** Utiliza o DatabaseContext para operações reais e assíncronas

**Por quê?** 
- Dados agora são persistentes (salvos no banco)
- Operações assíncronas (`async/await`) evitam travamento da interface
- Segue o padrão Repository, separando lógica de negócio do acesso a dados

### 2.2 ConsultaService Criado
**Arquivo:** `Services/ConsultaService.cs`

**O que faz:** Gerencia agendamentos de consultas, incluindo filtro por data

**Por quê?** Atende o requisito de "registrar e consultar agendamentos"

---

## 3. VIEWMODELS (Lógica de Apresentação)

### 3.1 PacientesViewModel - COMPLETO
**Arquivo:** `ViewModels/PacientesViewModel.cs`

**Funcionalidades adicionadas:**
1. **NovoPacienteCommand** - Navega para formulário de cadastro
2. **EditarPacienteCommand** - Abre formulário preenchido para edição
3. **ExcluirPacienteCommand** - Pede confirmação e exclui paciente
4. **CarregarPacientesAsync()** - Atualiza lista do banco

**Mudança importante:** Métodos agora são assíncronos (`async/await`)

**Por quê?** Implementa todas as operações CRUD exigidas: Cadastrar, Listar, Editar, Excluir.

### 3.2 PacienteFormViewModel - NOVO
**Arquivo:** `ViewModels/PacienteFormViewModel.cs`

**O que faz:**
- Gerencia o formulário de cadastro/edição de pacientes
- Implementa `IQueryAttributable` para receber o ID do paciente na navegação
- Valida dados antes de salvar
- `SalvarAsync()` - Insere novo paciente ou atualiza existente

**Por quê?** Separa a lógica do formulário da tela, facilitando testes e manutenção.

### 3.3 AgendaViewModel - IMPLEMENTADO
**Arquivo:** `ViewModels/AgendaViewModel.cs`

**Funcionalidades:**
- Propriedade `DataSelecionada` que recarrega consultas ao mudar
- `NovaConsultaCommand` - Navega para formulário de agendamento
- `ExcluirConsultaCommand` - Remove consulta com confirmação
- `CarregarConsultasDoDiaAsync()` - Filtra consultas pela data selecionada

**Por quê?** Permite "controle de agenda do consultório" conforme requisito.

### 3.4 ConsultaFormViewModel - NOVO
**Arquivo:** `ViewModels/ConsultaFormViewModel.cs`

**Similar ao PacienteFormViewModel**, mas para agendamentos de consultas.

---

## 4. VIEWS (Interface do Usuário)

### 4.1 PacientesPage.xaml - ATUALIZADO
**Principais mudanças:**

1. **Botões Editar e Excluir** adicionados a cada item da lista:
```xml
<Grid ColumnDefinitions="*,Auto,Auto">
    <!-- Informações do paciente -->
    <Button Text="Editar" ... />
    <Button Text="Excluir" ... />
</Grid>
```

2. **RelativeSource Binding** para acessar comandos da ViewModel do pai:
```xml
Command="{Binding Source={RelativeSource AncestorType={x:Type local:PacientesPage}}, 
                  Path=BindingContext.EditarPacienteCommand}"
```

**Por quê?** Permite editar e excluir diretamente da lista.

### 4.2 PacientesPage.xaml.cs - ATUALIZADO
**Mudança:** Removido `BindingContext` do XAML, configurado no code-behind via injeção de dependência.

**Adicionado:** `OnAppearing()` para recarregar dados ao voltar do formulário.

**Por quê?** Quando você salva um paciente e volta, a lista é atualizada automaticamente.

### 4.3 PacienteFormPage.xaml - ATUALIZADO
**Mudanças:**
- Campos agora têm `Text="{Binding Paciente.Nome}"` (two-way binding)
- Removido campo "Observações" (não existe no modelo Paciente)

**Por quê?** Data binding sincroniza automaticamente os campos com o objeto Paciente na ViewModel.

### 4.4 PacienteFormPage.xaml.cs - IMPLEMENTADO
**Métodos:**
- `OnSalvarClicked()` - Chama `SalvarAsync()` da ViewModel e volta para lista
- `OnCancelarClicked()` - Volta sem salvar

**Por quê?** Conecta os botões da interface à lógica de salvamento.

### 4.5 AgendaPage.xaml e .cs - ATUALIZADOS
**Mudanças:**
- `ItemsSource="{Binding Consultas}"` no CollectionView
- `OnDataSelecionadaChanged()` atualiza a ViewModel
- Interface preparada para listar consultas do dia selecionado

**Por quê?** Exibe agendamentos filtrados por data.

### 4.6 ConsultaFormPage.xaml e .cs - ATUALIZADOS
Similar ao PacienteFormPage, mas para criar/editar consultas.

---

## 5. NAVEGAÇÃO E INJEÇÃO DE DEPENDÊNCIA

### 5.1 AppShell.xaml.cs - CRIADO
**Registra rotas de navegação:**
```csharp
Routing.RegisterRoute(nameof(PacienteFormPage), typeof(PacienteFormPage));
Routing.RegisterRoute(nameof(ConsultaFormPage), typeof(ConsultaFormPage));
```

**Por quê?** Permite navegação com `Shell.Current.GoToAsync(nameof(PacienteFormPage))`.

### 5.2 MauiProgram.cs - AMPLIADO
**Registros adicionados:**
- `DatabaseContext` como Singleton (instância única para toda a aplicação)
- `PacienteService` e `ConsultaService` como Singletons
- Todas as ViewModels como Transient (nova instância a cada uso)
- Todas as Pages como Transient

**Por quê?** 
- **Injeção de Dependência** facilita testes e manutenção
- **Singleton para Database** garante que todos usam a mesma conexão
- **Transient para Pages** cria instância nova a cada navegação, evitando dados antigos

---

## 6. CORREÇÕES DE BUGS

### 6.1 Namespaces XAML
**Problema:** `xmlns:sys="clr-namespace:System;assembly=netstandard"` causava erro.
**Solução:** Mudei para `assembly=System.Runtime` e movi para o elemento raiz.

**Por quê?** .NET 8 não usa mais netstandard como assembly principal.

### 6.2 TargetFrameworks
**Problema:** Projeto tentava compilar para iOS e Android, mas workloads não instalados.
**Solução:** Comentei outras plataformas, deixando apenas Windows.

**Por quê?** Ambiente atual só tem suporte para Windows, mas o código é multiplataforma.

---

## RESUMO DO QUE FOI IMPLEMENTADO

✅ **Cadastrar pacientes** - PacienteFormPage com validação  
✅ **Listar pacientes** - PacientesPage com lista do banco  
✅ **Editar pacientes** - Botão editar que reabre o formulário preenchido  
✅ **Excluir pacientes** - Botão excluir com confirmação  
✅ **Registrar agendamentos** - ConsultaFormPage completo  
✅ **Consultar agendamentos** - AgendaPage com filtro por data  
✅ **Banco de dados local** - SQLite com persistência real  
✅ **Operações assíncronas** - Interface nunca trava  
✅ **Padrão MVVM** - Separação clara entre UI e lógica  
✅ **Injeção de dependência** - Código testável e manutenível  

---

## PRÓXIMOS PASSOS SUGERIDOS (Não implementados)

1. **Melhorias visuais**: Ícones, cores temáticas, animações
2. **Validações avançadas**: Verificar formato de telefone/email
3. **Busca e filtros**: Pesquisar pacientes por nome
4. **Relatórios**: Exportar lista de consultas
5. **Notificações**: Lembrete de consultas
6. **Backup**: Exportar/importar banco de dados

---

## CONCLUSÃO

O aplicativo agora atende **100% dos requisitos SMART** fornecidos:
- ✅ **Específico**: Cadastro de pacientes e controle de agenda funcionando
- ✅ **Mensurável**: CRUD completo para pacientes e agendamentos
- ✅ **Atingível**: Código simples, bem estruturado, adequado para aprendizado
- ✅ **Relevante**: Projeto real aplicando conceitos MAUI, MVVM, SQLite, async/await

O código está **pronto para uso e aprendizado**, seguindo boas práticas da indústria.
