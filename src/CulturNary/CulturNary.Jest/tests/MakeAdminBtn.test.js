// Mock global functions
global.alert = jest.fn();

// Mock location.reload
global.location = { reload: jest.fn() };

// Mock jQuery and its methods
const $ = require('jquery');
$.ajax = jest.fn().mockImplementation(() => Promise.resolve());
$.ready = jest.fn().mockImplementation((fn) => fn());
$.click = jest.fn().mockImplementation((fn) => fn());
$.data = jest.fn().mockReturnValue('123');
global.$ = $;

// Import the functions from MakeAdminBtn.js
const {handleError, setupClickHandler } = require('../../CulturNary.Web/wwwroot/js/MakeAdminBtn');

beforeEach(() => {
    jest.clearAllMocks();
});

describe('handleError', () => {
    it('shows an error alert', () => {
        handleError();
        expect(alert).toHaveBeenCalledWith('Error making user admin');
    });
});

describe('setupClickHandler', () => {
    it('sets up the click handler correctly', () => {
        const mockClick = jest.fn();
        $.fn.click = mockClick;
        setupClickHandler();
        expect(mockClick).toHaveBeenCalled();
    });
});