# Utilities

Utility Library which provides extension methods sitting on top of the NET standard library and utility classes.


## Collections
### Each
Allows looping in linq-style.

- Each
'''csharp
collection.Each(p_item => p_item.DoSomething());
'''

- Each with index
'''csharp
collection.Each((p_item, p_index) => Console.WriteLine($@"Item: {p_index} at index {p_index}"));
'''


## Persistence
(inspired by dapper)

### Execute
Allows executing any statement, without handling connections.

- Execute
'''csharp
int numberOfAffectedRows = Persistence.Execute(connectionString,sql);
'''
- ExecuteInTransaction
'''csharp
int numberOfAffectedRows = Persistence.ExecuteInTransaction(connectionString,sql);
'''

### Query
- Query
Returns a list of all matching entities
'''csharp
IList<TestEntity> testEntities = Persistence.Query<TestEntity>(connectionString, sql);
'''

- QueryFirst
Returns null when result not found
'''csharp
TestEntity test = Persistence.QueryFirst<TestEntity>(connectionString, sql);
'''


- QueryFirstOrDefault
Returns empty object with default values initialized for all properties
'''csharp
TestEntity test = Persistence.QueryFirstOrDefault<TestEntity>(connectionString, sql);
'''

