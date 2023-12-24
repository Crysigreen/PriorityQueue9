using System;
using System.Collections.Generic;

public class PriorityQueue<E, P> where E : IComparable<E>
{
    private SortedDictionary<P, Queue<E>> priorityQueueMap;

    public PriorityQueue()
    {
        priorityQueueMap = new SortedDictionary<P, Queue<E>>();
    }

    public int Size
    {
        get
        {
            int count = 0;
            foreach (var queue in priorityQueueMap.Values)
            {
                count += queue.Count;
            }
            return count;
        }
    }

    public void Add(E element, P priority)
    {
        if (!priorityQueueMap.TryGetValue(priority, out Queue<E> queue))
        {
            queue = new Queue<E>();
            priorityQueueMap[priority] = queue;
        }

        queue.Enqueue(element);
    }

    public IEnumerable<E> GetAll()
    {
        foreach (var queue in priorityQueueMap.Values)
        {
            foreach (var element in queue)
            {
                yield return element;
            }
        }
    }

    public E Peek()
    {
        if (priorityQueueMap.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty.");

        foreach (var queue in priorityQueueMap.Values)
        {
            if (queue.Count > 0)
                return queue.Peek();
        }

        throw new InvalidOperationException("PriorityQueue is empty.");
    }

    public E Poll()
    {
        if (priorityQueueMap.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty.");

        foreach (var queue in priorityQueueMap.Values)
        {
            if (queue.Count > 0)
            {
                E element = queue.Dequeue();
                if (queue.Count == 0)
                {
                    priorityQueueMap.Remove(priorityQueueMap.First().Key);
                }
                return element;
            }
        }

        throw new InvalidOperationException("PriorityQueue is empty.");
    }
}

public class Program
{
    public static void Main()
    {
        // Пример использования PriorityQueue с поддержкой получения всех элементов
        PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int>();

        priorityQueue.Add("Task 1", 2);
        priorityQueue.Add("Task 2", 1);
        priorityQueue.Add("Task 3", 2);
        priorityQueue.Add("Task 4", 3);
        priorityQueue.Add("Task 5", 1);

        Console.WriteLine("Peek: " + priorityQueue.Peek()); // Вывод: Task 2

        Console.WriteLine("GetAll:");
        foreach (var element in priorityQueue.GetAll())
        {
            Console.WriteLine(element);
        }

        Console.WriteLine("Poll: " + priorityQueue.Poll()); // Вывод: Task 2

        Console.WriteLine("GetAll:");
        foreach (var element in priorityQueue.GetAll())
        {
            Console.WriteLine(element);
        }
    }
}
