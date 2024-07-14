using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Domain.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<ToDoItemModel> ToDoItems { get; set; }
}

