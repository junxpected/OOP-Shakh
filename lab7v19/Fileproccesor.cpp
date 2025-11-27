// імітує читання дампу з "бази"
// перші 2 рази падає, потім успіх
class FileProcessor {
    int fails = 0;
public:
    vector<string> ReadDatabaseDump(const string& path) {
        if (fails < 2) {
            fails++;
            throw runtime_error("File not found");
        }
        return { "user1", "user2", "user3" };
    }
};
