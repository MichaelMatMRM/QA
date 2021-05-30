using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace HashTableShared {
    public class Element {
        public string key = "";
        public string value = "";

        public Element(string key = "", string value = "") {
            this.key = key;
            this.value = value;
        }
    }

    public class HashTable {
        List<List<Element>> table;
        public int Size { get; }


        public HashTable(int inputSize = 1000) {
            Size = inputSize;
            table = new List<List<Element>>(inputSize);
            while (inputSize-- != 0) {
                table.Add(new List<Element> { new Element() });
            }
        }

        private int HashCode(string key, int a) =>
            (HashCode1(key) + a * HashCode2(key)) % Size;
   

        private int HashCode1(string key) =>
            (key[0] * key[1] * 2 + key[1] * key[2] * 10 + key[2] * key[3] * 27 + key[3] * key[4] * 82 + key[4] * key[5] * 242) % Size;
       

        private int HashCode2(string key) =>
            (key[0] * 3 + key[1] * 9 + key[2] * 27 + key[3] * 81 + key[4] * 243 + key[5] * 729) % Size;

        private static void KeyIsCorrect(string key) {
            if (key[0] < 'A' || key[0] > 'Z' ||
                key[1] < 'A' || key[1] > 'Z' ||
                key[2] < '0' || key[2] > '9' ||
                key[3] < '0' || key[3] > '9' ||
                key[4] < 'A' || key[4] > 'Z' ||
                key[5] < 'A' || key[5] > 'Z') {
                throw new ArgumentException();
            }
        }

        private void Add(Element toAdd) {
            KeyIsCorrect(toAdd.key);

            var count = 0;
            while (count != Size * 3) {
                if (table[HashCode(toAdd.key, count)][0].key == toAdd.key) {
                    table[HashCode(toAdd.key, count)].Add(toAdd);
                    break;
                }
                // В этой строке поменять == на !=

                if (table[HashCode(toAdd.key, count)][0].key == "") {
                    table[HashCode(toAdd.key, count)][0] = toAdd;
                    break;
                }
                count++;
            }

            if (count == Size * 3) {
                throw new CheckoutException();
            }
        }


        public string this[string key] {
            set => Add(new Element(key, value));

            get {
                KeyIsCorrect(key);

                var count = 0;
                while (count != Size * 3) {
                    if (table[HashCode(key, count)][0].key == key) {
                        break;
                    }

                    count++;
                }

                if (count == Size * 3) {
                    throw new CheckoutException();
                }

                return table[HashCode(key, count)].FirstOrDefault()?.value;
            }
        }
    }
}

