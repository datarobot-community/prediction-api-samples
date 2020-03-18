import io.github.cdimascio.dotenv.Dotenv;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;

public class Predictions {

    static Dotenv dotenv = Dotenv.configure()
            .directory("../common")
            .load();

    static String PREDICTION_SERVER = dotenv.get("PREDICTION_SERVER");
    static String DEPLOYMENT_ID = dotenv.get("DEPLOYMENT_ID");
    static String PREDICTION_ENDPOINT = PREDICTION_SERVER + "/predApi/v1.0/deployments/" + DEPLOYMENT_ID + "/predictions";
    static String API_KEY = dotenv.get("API_KEY");
    static String DATAROBOT_KEY = dotenv.get("DATAROBOT_KEY");

    public static void main(String[] args){
        try {
            String payload = Files.readString(Paths.get("../common/payload_to_predict.json"), StandardCharsets.UTF_8);

            var request = HttpRequest.newBuilder(URI.create(PREDICTION_ENDPOINT))
                    .header("Content-Type", "application/json")
                    .header("charset", "UTF-8")
                    .header("datarobot-key", DATAROBOT_KEY)
                    .header("Authorization", "Bearer " + API_KEY)
                    .POST(HttpRequest.BodyPublishers.ofString(payload))
                    .build();

            var client = HttpClient.newHttpClient();

            var response = client.send(request, HttpResponse.BodyHandlers.ofString());

            System.out.println(response.statusCode());
            System.out.println(response.body());

        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
        }
    }
}
