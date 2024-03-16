//mock jquery
global.$ = jest.fn(() => ({
    ready: jest.fn(),
    data: jest.fn(),
    text: jest.fn(),
    empty: jest.fn(),
    append: jest.fn(),
}));

const { fetchUserDetails, fetchCollections } = require('../../CulturNary.Web/wwwroot/js/ModalAdmin');

describe('fetchUserDetails', () => {
    beforeEach(() => {
        global.fetch = jest.fn(() =>
            Promise.resolve({
                json: () => Promise.resolve({ id: '123', userName: 'test' }),
            })
        );
    });

    it('calls fetch with the correct url', () => {
        fetchUserDetails('123');
        expect(fetch).toHaveBeenCalledWith('/Admin/Users/123');
    });

    it('handles fetch errors', async () => {
        global.fetch = jest.fn(() => Promise.reject('Fetch error'));
        await expect(fetchUserDetails('123')).rejects.toEqual('Fetch error');
    });

    it('correctly processes the fetched data', async () => {
        const data = await fetchUserDetails('123');
        expect(data).toEqual({ id: '123', userName: 'test' });
    });
});

describe('fetchCollections', () => {
    beforeEach(() => {
        global.fetch = jest.fn(() =>
            Promise.resolve({
                json: () => Promise.resolve([{ id: '1', name: 'Collection 1' }]),
            })
        );
    });

    it('calls fetch with the correct url', () => {
        fetchCollections('123');
        expect(fetch).toHaveBeenCalledWith('/api/Collection/123');
    });

    it('throws an error when personId is undefined', () => {
        expect(() => fetchCollections()).toThrow('PersonId is undefined');
    });

    it('handles fetch errors', async () => {
        global.fetch = jest.fn(() => Promise.reject('Fetch error'));
        await expect(fetchCollections('123')).rejects.toEqual('Fetch error');
    });

    it('correctly processes the fetched data', async () => {
        const data = await fetchCollections('123');
        expect(data).toEqual([{ id: '1', name: 'Collection 1' }]);
    });
});