const { deleteUser, handleSuccess, handleError, setupClickHandler, initialize } = require('../../CulturNary.Web/wwwroot/js/DeleteUserBtn');

describe('DeleteUserBtn', () => {
    let mockAjax, mockReload, mockGetUserId, mockElement;

    beforeEach(() => {
        mockAjax = jest.fn();
        mockReload = jest.fn();
        mockGetUserId = jest.fn();
        mockElement = {};
        global.alert = jest.fn();
    });

    it('deleteUser sends an AJAX request with the correct parameters', () => {
        deleteUser('testUserId', mockAjax);
        expect(mockAjax).toHaveBeenCalledWith({
            url: '/Admin/DeleteUser/testUserId',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ userId: 'testUserId' }),
        });
    });

    it('handleSuccess shows an alert and reloads the page', () => {
        handleSuccess(mockReload);
        expect(global.alert).toHaveBeenCalledWith('User has been deleted');
        expect(mockReload).toHaveBeenCalled();
    });

    it('handleError shows an alert', () => {
        handleError();
        expect(global.alert).toHaveBeenCalledWith('Error deleting user');
    });

    it('setupClickHandler returns a function that gets the user ID, deletes the user, and handles success or error', async () => {
        mockGetUserId.mockReturnValue('testUserId');
        mockAjax.mockResolvedValue();
        const clickHandler = setupClickHandler(mockGetUserId, mockAjax, handleSuccess, handleError);
        await clickHandler.call(mockElement);
        expect(mockGetUserId).toHaveBeenCalledWith(mockElement);
        expect(mockAjax).toHaveBeenCalled();
    });
    
    it('initialize sets up the click handler', () => {
        const mockReady = jest.fn(cb => cb());
        const mockClick = jest.fn();
        global.$ = jest.fn(() => ({
            ready: mockReady,
            click: mockClick,
        }));
        initialize(setupClickHandler);
        expect(mockReady).toHaveBeenCalled();
        expect(mockClick).toHaveBeenCalled();
    });
});