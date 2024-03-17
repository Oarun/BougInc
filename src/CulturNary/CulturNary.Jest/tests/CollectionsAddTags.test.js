// Mock global functions
global.alert = jest.fn();
//mock jquery and its methods
const $ = require('jquery');
$.ajax = jest.fn().mockImplementation(() => Promise.resolve());
$.ready = jest.fn().mockImplementation((fn) => fn());
$.click = jest.fn().mockImplementation((fn) => fn());
$.data = jest.fn().mockReturnValue('123');
$.val = jest.fn(selector => {
    return 'Mock Value';
  });
$.empty = jest.fn();
$.append = jest.fn();
$.on = jest.fn();
$.each = jest.fn();
$.html = jest.fn();
$.remove = jest.fn();
$.hide = jest.fn();
$.show = jest.fn();
global.$ = $;

global.updateTags = jest.fn();
global.getCollection = jest.fn();

const { putTags, updateTags, getCollection } = require('../../CulturNary.Web/wwwroot/js/Collections');

describe('putTags Function', () => {
    // Mock global variables and functions
    const mockSuccessCallback = jest.fn();
    const mockErrorCallback = jest.fn();
    const currentCollectionId = '1';

    beforeEach(() => {
        jest.clearAllMocks();

        $.val.mockImplementation(selector => {
            return 'Mock Value';
        });
    });

    it('$.val returns the correct value', () => {
        const result = $.val('#collectionName');
        expect(result).toBe('Mock Value');
    });

    it('calls $.ajax with the correct parameters', () => {
        const updatedTags = ['tag1', 'tag2'];
        putTags(updatedTags, global.currentCollectionId);

        expect($.ajax).toHaveBeenCalledWith(expect.objectContaining({
            url: '/api/Collection/Tags/' + global.currentCollectionId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                id: global.currentCollectionId,
                name: 'Mock Value', // Assuming $('#collectionName').val() returns 'Mock Value'
                description: 'Mock Value', // Assuming $('#collectionDescription').val() returns 'Mock Value'
                tags: updatedTags
            }),
        }));
    });

});
