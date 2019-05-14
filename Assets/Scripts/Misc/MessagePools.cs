using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MessagePools
{

    public static string helpMessage =
                        "Global commands:\n" +
                        "Music On / Off\n" +
                        "Help (but you know that already)\n" +
                        "Get stats\n" +
                        "Pretty\n" +
                        "Exit\n";

    public static string[] mobHit =
    {
        "You deal damage!\n",
        "You hit really hard!\n",
        "That look like it hurt...\n",
        "Good job! You're doing great!\n",
        "What a blow...\n",
        "That looked painful.\n",
        "That must have hurt.\n",
        "The fireball hits its mark!",
        "Your arrows fly true!\n",
        "COWABUNGA\n",
        "You thrust your sword forward and pierce!\n",
        "You throw your granade and deal TONS OF DAMAGE.\n",
        "You punch. It's super effective!\n",
        "You have the high ground and deal a major blow.\n",
        "That...shouting...is dealing a lot of damage...\n",
        "You stick the enemy with the pointy end.\n",
        "Nothing is more badass than damaging an enemy with respect.\n",
        "You shoot the enemy with an arrow to the knee.\n",

    };

    public static string[] mobMiss =
    {
        "You missed!\n",
        "You tried but failed.\n",
        "That was a miss!\n",
        "Nooo, you failed this attack, try again!\n",
        "That wasn't successful.\n",
        "Your attack was like my love life in highschool. Not good.\n",
        "You are an amature boxer and the enemy is Mohamed Ali. You missed everything.\n",
        "Your attack and the dot-com crash have many things in common.\n",
        "*YOU DIED*\n(but not really, you can still try)\n",
        "SUCCESS! But it's opposite day.\n",
        "Hit or miss, I guess you always miss, huh?\n",
        "Git gud.\n",
        "Your aim is only matched by imperial stormtroopers.\n",
        "Would you like me to lower the difficulty?\n"
    };

    public static string[] mobDead =
    {
        "You dealt the final blow, good job!\n",
        "Nicely done, you have defeated the enemy.\n",
        "You finish the job with a majestic uppercut!\n",
        "This was too much for your enemy. They fell, bravo.\n",
        "The cake is a lie, but your might isn't! You defeat your enemy!\n",
        "Stop! You violated the law!..but you did kill the enemy\n",
        "You do what you must because you can. Enemy slain.\n",
        "The enemy died of dysentery.\n",
        "THE NUMBERS, PLAYER. WHAT DO THEY MEAN? They mean the enemy is dead!\n",
        "You stand in the ashes of a trillion dead souls...you beat them all!\n",
        "Your strong heart defeated your enemy! Battle is more than skill!\n",
        "The cycle of life and death continues, you will live, they died.\n",
        "There is only one thing you say to the god of death: here is another one.\n",
        "It was time to kick ass and chew bubble gum...and you were all out of bubble gum.\n",
        "When the snows fall and the white winds blow, the lone enemy dies and the player survives.\n"
    };

    public static string[] emptyAttack =
    {
        "Nothing to attack",
        "You talking to me? YOU TALKING TO ME? Because I see no one else here...",
        "I see dead...actually I see don't see anything, what are you doing?",
        "Practicing for a mob encounter? Good job, you learned how to attack!",
        "Well...as long as it is just your SWORD you're waving around, it's fine.",
        "Uh...okay...good job doing nothing...I guess...",
        "Passive agressive message telling you to stop attacking thin air.",
        "Rehearsing for a play I see.",
        "*You wave your sword around while grunting, everyone just awkwardly stares at you*",
        "Are...are you ok?",
        "You:  *attacks nothing*\nLife: *nothing happens*\nYou: :O",
        "You are not going to become a great warrior by poking at nothing.",
        "GO! YES! ATTACK THE AIR, SHOW IT WHAT YOU GOT!",
        "Did you hurt yourself in confusion?",
        "Does anyone know any good jokes while this one does nothing?..."
    };

    public static string[] emptyTask =
    {
        "No task to open",
        "There is literally nothing here...",
        "Nothing to see here, keep moving.",
        "*You mimic a task opening gesture and stare in confusion as nothing happens*",
        "Have you read the book called isuhvndaibanduivwb? No? That's because it doesn't exist. Just like the task you tried to open.",
        "NEW TASK! GO FIND AN ACTUAL TASK! QUICKLY!",
        "No task here, but if you really want one, why don't you bring my creator a beer?",
        "Why did the player cross the road? To find a task.",
    };

    public static string GetMsg(string[] l)
    {
        return l[Random.Range(0, l.Length)];
    }
}
