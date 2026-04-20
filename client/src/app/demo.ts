let message ="Hello";
let isComplete = false;

interface Todo{
    id: number;
    title: string;
    completed: boolean;
}

let todos: Todo[] = [];

function addTodo(title: string): Todo {
    const newTodo: Todo = {
        id: todos.length + 1,
        title,
        completed: false
    };
    todos.push(newTodo);
    return newTodo;
}

function toggleTodo(id: number): void {
    const todo = todos.find(t => t.id === id);
    if (todo) {
        todo.completed = !todo.completed;
    }
}

addTodo("Learn TypeScript");
addTodo("Build an Angular app");
toggleTodo(1);
console.log(todos);