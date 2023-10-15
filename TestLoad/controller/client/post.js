import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 10000, // Número de usuários virtuais (VUs)
  duration: '60s', // Duração do teste
  thresholds: {
    http_req_failed: ['rate<0.01'],
    http_req_duration: ['p(95)<200'],
  }
};

export default async function () {
  const url = 'http://localhost:5141/api/clients';

  const payload = JSON.stringify({
    name: 'maria',
    email: 'maria@email.com',
    logourl: '',
  });

  const headers = {
    'Content-Type': 'application/json',
  };

  let res = http.post(url, payload, { headers: headers });

  check(res, {
    'Status is 201': (r) => r.status === 201,
  });

  sleep(1);
}
