# Второе задание
1. Используемые алгоритмы: 
- AES — симметричный алгоритм блочного шифрования, принятый в качестве стандарта шифрования правительством США по результатам конкурса AES. Достоинствами являются:
    - Безопасный
    - Бесплатный
    - Оптимизированный. Алгоритм эффективно использует вычислительные мощности, поэтому не требователен к оперативной памяти устройства и «железу»
    - Мультиплатформенный. Реализовывается как на аппаратной части, так и на программной
- RSA — криптографический алгоритм с открытым ключом, основывающийся на вычислительной сложности задачи факторизации больших целых чисел. Достоинствами являются:
    - Скорость
    - Простота реализации (за счёт более простых операций)
    - Меньшая требуемая длина ключа для сопоставимой стойкости
    - Изученность (за счёт большего возраста)
2. С использованием публичного ключа RSA шифруется ключ для AES и записывается в зашифрованный файл, сразу после ключа IV. Ключи RSA хранятся в защищенных контейнерах. ContainerPrivate.txt для приватных ключей, ContainerPublic.txt для публичных.
3. Шифрование происходит побайтово, что позволяет сохранять данные в целосности. Эмпирическим путем было проверено, что программа способна шифровать и расшифровывать без потери данных, следующие форматы файлов: .txt, .docx, .png
4. Генерация и создание ключей описаны в главе "Пояснения к элементам формы"

## Пояснения к элементам формы

Форма содержит следующие кнопки:
- Путь к файлу – путь к файлу, над которым будет происходить работа;
- Название ключа – название ключа, под которым этот ключ будет хранится в контейнерах;
- Уведомления – вывод сообщений для пользователя;
- Шифрование – шифрует файл при наличии публичного ключа;
- Расшифровать -  расшифровует файл при наличии закрытого ключа;
- Создать приватный ключ -  создает закрытый ключ и сохраняет его в ContainerPrivate.txt, который шифруется стандартными средствами Windows;
- Сохранить публичны ключ -  сохраняет публичный ключ в ContainerPublic.txt, который шифруется стандартными средствами Windows;
- Загрузить приватный ключ -  загружает закрытый ключ в систему из ContainerPrivate;
- Сохранить публичны ключ -  загружает публичный ключ в систему из ContainerPublic.

Формы \
![Форма программы и содержимое файла после шифрования](https://github.com/VladimirNone/OlympPetersburg/blob/main/Scrinshots/1.png?raw=true) \
Рисунок 1 – Форма программы и содержимое файла после шифрования
 
Рисунок 2 – Содержимое файла ContainerPrivate.txt

Рисунок 3 – Сохранение публичного ключа и содержимое файла ContainerPublic.txt

Рисунок 4 – Форма программы после перезапуска, загрузки публичного ключа и шифрования файла, содержимое файла после шифрования

Рисунок 5 – Содержимое директорий после работы программы (созданные файлы автоматически шифруются средствами windows, по этой причине видны «ключики» на иконках)