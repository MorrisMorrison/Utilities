# Utilities

Utility Library which provides extension methods for the NET standard library.


## Collections
### Each
Allows looping in linq-style.

- Each
```csharp
collection.Each(p_item => p_item.DoSomething());
```

- Each with index
```csharp
collection.Each((p_item, p_index) => Console.WriteLine($@"Item: {p_index} at index {p_index}"));
```


## Persistence
(inspired by dapper)

### Execute
Allows executing any statement

- Execute
```csharp
using (SqlConnection connection = new SqlConnection(connectionString){
	int numberOfAffectedRows = connection.Execute(sql);
}

```
- ExecuteInTransaction
```csharp
using (SqlConnection connection = new SqlConnection(connectionString){
	int numberOfAffectedRows = connection.ExecuteInTransaction(sql);
}
```

### Query
- Query
Returns a list of all matching entities
```csharp
using (SqlConnection connection = new SqlConnection(connectionString){
	IList<TestEntity> testEntities = connection.Query<TestEntity>(sql);
}
```

- QueryFirst
Returns null when result not found
```csharp
using (SqlConnection connection = new SqlConnection(connectionString){
	TestEntity test = connection.QueryFirst<TestEntity>(sql);
}
```

- QueryFirstOrDefault
Returns empty object with default values initialized for all properties
```csharp
using (SqlConnection connection = new SqlConnection(connectionString){
	TestEntity test = connection.QueryFirstOrDefault<TestEntity>(sql);
}
```

## XML

### XDocumentExtensions
Allows finding elements in a more convenient way

- FindElements
```csharp
xDocument.FindElements("ElementName");
xDocument.FindElements("ElementName", "AttributeName");
xDocument.FindElements("ElementName", "AttributeName", "AttributeValue");
```

- FindElementsByAttribute
```csharp
xDocument.FindElementsByAttribute("AttributeName");
xDocument.FindElementsByAttribute("AttributeName", "AttributeValue");
```


### XElementExtensions
Adds Helper Methods for XElement

```csharp
xElement.HasName("ElementName");
xElement.HasAttribute("AttributeName");
xElement.HasAttributeWithValue("AttributeName", "AttributeValue");
```


