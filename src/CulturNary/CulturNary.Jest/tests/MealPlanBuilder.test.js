test('CalculateTotalCalories function should return the correct total calories for a list of meals', () => {
    const meals = [
        { name: 'Breakfast', calories: 300 },
        { name: 'Lunch', calories: 500 },
        { name: 'Dinner', calories: 700 }
    ];
    const totalCalories = calculateTotalCalories(meals);
    expect(totalCalories).toBe(1500);
});

test('FetchMealOptions function should retrieve meal options from the API', async () => {
    const mealOptions = await fetchMealOptions();
    expect(mealOptions).toHaveLength(3);
    expect(mealOptions[0].name).toBe('Omelette');
    expect(mealOptions[1].name).toBe('Grilled Chicken Salad');
    expect(mealOptions[2].name).toBe('Vegetable Stir-Fry');
});

test('User can save a generated meal plan to their profile', async () => {
    // Simulate user actions to generate a meal plan
    render(<MealPlanBuilder />);
    fireEvent.click(screen.getByText('Build'));
    await waitFor(() => screen.getByText('Meal Plan Generated'));
    // Simulate user action to save the meal plan
    fireEvent.click(screen.getByText('Save'));
    await waitFor(() => screen.getByText('Meal Plan Saved'));
    // Verify the meal plan is saved to the user's profile
    const savedMealPlan = await getSavedMealPlan();
    expect(savedMealPlan).not.toBeNull();
});

test('MealPlanBuilder component renders correctly', () => {
    render(<MealPlanBuilder />);
    expect(screen.getByText('Build')).toBeInTheDocument();
    expect(screen.getByText('Number of Meals:')).toBeInTheDocument();
    expect(screen.getByText('Allergies:')).toBeInTheDocument();
    expect(screen.getByText('Diet:')).toBeInTheDocument();
    // Add more assertions as needed
});

test('MealPlanBuilder component matches snapshot', () => {
    const { asFragment } = render(<MealPlanBuilder />);
    expect(asFragment()).toMatchSnapshot();
});

test('All UI components are accessible', async () => {
    const { container } = render(<MealPlanBuilder />);
    const results = await axe(container);
    expect(results).toHaveNoViolations();
});
