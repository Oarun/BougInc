// ModalAdmin.test.js
global.fetch = jest.fn(() =>
    Promise.resolve({
        json: () => Promise.resolve({ id: '123', userName: 'test' }),
    })
);
global.$ = jest.fn(() => ({
    ready: jest.fn(),
    data: jest.fn(),
    text: jest.fn(),
    empty: jest.fn(),
    append: jest.fn(),
  }));
  
global.fetch = jest.fn(() => Promise.resolve({
    json: () => Promise.resolve({}),
  }));
// Mock jQuery
global.$ = jest.fn(() => ({
    ready: jest.fn(),
  }));

const { fetchUserDetails, fetchCollections } = require('../../CulturNary.Web/wwwroot/js/ModalAdmin');

describe('fetchUserDetails', () => {
    it('calls fetch with the correct url', () => {
        fetchUserDetails('123');
        expect(fetch).toHaveBeenCalledWith('/Admin/Users/123');
    });
});

describe('fetchCollections', () => {
    it('calls fetch with the correct url', () => {
        fetchCollections('123');
        expect(fetch).toHaveBeenCalledWith('/api/Collection/123');
    });

    it('throws an error when personId is undefined', () => {
        expect(() => fetchCollections()).toThrow('PersonId is undefined');
    });
});