// імітує запит до "віддаленої бази"
// перші 3 рази падає, потім успіх
class NetworkClient {
    int fails = 0;
public:
    vector<string> QueryDatabase(const string& conn, const string& q) {
        if (fails < 3) {
            fails++;
            throw runtime_error("Network error");
        }
        return { "remote1", "remote2" };
    }
};
