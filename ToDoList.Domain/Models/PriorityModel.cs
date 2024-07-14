using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Domain.Models;

public class PriorityModel
{
    public int Id { get; set; }
    public int Level { get; set; }
    public virtual List<ToDoItemModel> ToDoItems { get; set; }
}
