import http from "k6/http";
import { check, sleep } from "k6";

// Test configuration
export const options = {
  thresholds: {
    // Assert that 99% of requests finish within 3000ms.
    http_req_duration: ["p(99) < 1000"],
  },
  // Ramp the number of virtual users up and down
  stages: [
    { duration: "1m", target: 3000 },
    { duration: "1m", target: 3000 },
    { duration: "30", target: 0 },
  ],
};

// Simulated user behavior
export default function () {
  let res = http.get("https://localhost:5001/");
  // Validate response status
  check(res, { "status was 200": (r) => r.status == 200 });
  sleep(1);
}