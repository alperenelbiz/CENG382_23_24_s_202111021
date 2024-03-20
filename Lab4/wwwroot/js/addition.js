const toggleButton = document.getElementById("toggleButton");
const content = document.getElementById("content");
const content2 = document.getElementById("content2");

toggleButton.addEventListener("click", function () {
  if (content.style.display === "none") {
    content.style.display = "block";
  } else {
    content.style.display = "none";
  }
});

toggleButton2.addEventListener("click", function () {
  if (content2.style.display === "none") {
    content2.style.display = "block";
  } else {
    content2.style.display = "none";
  }
});

const calculateButton = document.getElementById("calculateButton");
const resultSpan = document.getElementById("result");

calculateButton.addEventListener("click", function () {
  const num1 = parseFloat(document.getElementById("num1").value);
  const num2 = parseFloat(document.getElementById("num2").value);

  if (isNaN(num1) || isNaN(num2)) {
    resultSpan.textContent = "Please enter valid numbers.";
  } else {
    const sum = num1 + num2;
    resultSpan.textContent = "The sum is: " + sum;
  }
});
