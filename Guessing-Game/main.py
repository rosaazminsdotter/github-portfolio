import csv
import random
import matplotlib.pyplot as plt

highScore = []
     
# Huvudmenyn som presenterar och hanterar de olika spelalternativen
def menu():
    while True:
        rubrik = "Start Menu"
        print(f"{rubrik:^20}")
        print(f"{'-'*20}") 
        print("1. Play")
        print("2. Show scores")
        print("3. Show diagram")
        print("4. Quit")
        print("PICK A NUMBER: ")
        choice = input()

        if choice == '1':
            play_game()
        elif choice == '2':
            print_scores()
        elif choice == '3':
            diagram()
        elif choice == '4':
            print("Tack för ditt deltagande. Dina resultat har sparats i resultatfilen")
            break
        else:
            print('Input not valid')

# Den första funktionen som kallas på och som hämtar in värdena från CSV-filen samt kallar på meny-funktionen
def start():
    import_scores()
    menu()

# En spel-funktion som kör självaste spelet
def play_game():
    score = 0
    doors = list(range(1,11))
    treasure = random.randint(1, 10)

    print('Pick a door')

    while True:
        for door in doors:
            print(door)
        guess = input()
        
        if guess == 'Quit' or guess == 'quit':
           break

        try:
            guess = int(guess)
        except ValueError:
            print("Only numbers allowed")
            continue
        
        if guess not in doors:
            print(f'{'Door '} {guess} {' is not an option'}')
        elif guess == treasure:
            score += 1
            print(f'{'You found the treasure behind door '}{treasure} {'after'} {score} {'tries!'}')
            name = input('Enter name: ')
            print(f'{name}{', your score is:'} {score}')
            add_scores(name, score)
            break
        else:
            doors.remove(guess)
            score += 1
            print('Try again')

# Denna funktion importerar värdena från CSV-filen, eller skapar en fil om den inte redan finns
def import_scores():
    try:
        file = open('skattresultat.csv','r')    
        csv_reader = csv.reader(file, delimiter = '')
        for rad in csv_reader:
            highScore.append(rad)
        file.close()
    except:
        file = open('skattresultat.csv','w')    
        highScore.append(["Name", "Tries"]) 
        write_to_csv() 


# En funktion som lägger till det nya high scoret till highScore-listan samt kallar på write_to_csv() 
def add_scores(name, score):       
    sort = False
    i = 1
    new = [name, score]
    length = len(highScore)
    while not sort and i <= length:
        if i == length:             # antingen så är filen tom, eller så har man nått slutet av listan
            highScore.append(new)
            sort = True
        else:
            old = highScore[i]
            if new[1] > int(old[1]): # konverterar till int då import från csv blir str
                i += 1
            else:
                highScore.insert(i, new)
                sort = True
    write_to_csv()

# En funktion som laddar upp listan highScore till CSV-filen
def write_to_csv():
    file = open('skattresultat.csv','w', newline ='')
    csv_writer = csv.writer(file, delimiter = ',')
    for rad in highScore:
        csv_writer.writerow(rad)
    file.close()

# En funktion som skriver ut poänglistan till skärmen
def print_scores():
    title = highScore[0]
    print(f"{title[0]:>8}{title[1]:>8}")
    print(f"{'-'*20}")
    length = len(highScore)
    i = 1

    while i < length:
        score = highScore[i]
        if len(score) >= 2: # Detta ifall formatet hade blivit fel, att score inte har [0] och [1], eller om listan highScore är tom så består CSV-filen enbart av en tom []
            print(f"{score[0]:>8}{score[1]:>8}", '\n')
        i += 1

# En funktion som visar ett diagram på listan    
def diagram():
    score_list = []
    frequencyList = []

    x = range(1, 11)

    # En while-loop som får fram en lista med bara poängen utan namn
    i = 1
    while i < len(highScore):
        score = highScore[i]
        if len(score) >= 2: 
            score_list.append(score[1])
        i += 1

    #En while-loop som får fram en lista med hur många gånger spelare har fått ett visst poäng
    i = 1
    while i < 11:
        index = 0
        amount = 0
        while index < len(score_list):
            if int(score_list[index]) == i:
                amount += 1
            index += 1
        frequencyList.append(amount)
        i += 1

    plt.title('Score diagram')
    plt.bar(x, frequencyList)
    plt.xlabel('Score')
    plt.ylabel('Frequency')
    plt.ylim(0, max(frequencyList) + 2)
    plt.show()

start()

