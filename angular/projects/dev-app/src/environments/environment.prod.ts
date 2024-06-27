import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'DynamicPermission',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44349/',
    redirectUri: baseUrl,
    clientId: 'DynamicPermission_App',
    responseType: 'code',
    scope: 'offline_access DynamicPermission',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44349',
      rootNamespace: 'JS.Abp.DynamicPermission',
    },
    DynamicPermission: {
      url: 'https://localhost:44397',
      rootNamespace: 'JS.Abp.DynamicPermission',
    },
  },
} as Environment;
